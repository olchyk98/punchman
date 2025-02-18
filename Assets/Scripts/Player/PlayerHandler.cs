﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(PlayerPowerup))]
    [RequireComponent(typeof(PlayerAnimations))]
    [RequireComponent(typeof(PlayerSoundSource))]
    [RequireComponent(typeof(PlayerBoundsCheck))] 
    public class PlayerHandler : MonoBehaviour
    {
        private PlayerHealth myHealth;
        private PlayerAttack myAttack;
        private PlayerMovement myMovement;
        private PlayerInputHandler myInputHandler;
        private PlayerAnimations myAnimations;
        private PlayerSoundSource mySoundSource;
        private PlayerBoundsCheck myBoundsCheck;

        public UnityAction<RaycastHit2D, Vector2, Attack> OnAttack;
        public UnityAction<int, float> OnKnockbackUpdate;
        public UnityAction<int> OnGameOver;

        public int Index { get; private set; }
        public float KnockbackPercentage {
            get => myHealth.KnockbackPercentage;
        }
        public float AmplificationPercentage = 1f;

        private void Start() {
            myInputHandler = GetComponent<PlayerInputHandler>();
            myMovement = GetComponent<PlayerMovement>();
            myHealth = GetComponent<PlayerHealth>();
            myAttack = GetComponent<PlayerAttack>();
            myAnimations = GetComponent<PlayerAnimations>();
            mySoundSource = GetComponent<PlayerSoundSource>();
            myBoundsCheck = GetComponent<PlayerBoundsCheck>();

            myBoundsCheck.OnOutOfBounds += HandleGameOver;

            myInputHandler.OnMove += HandleMove;
            myInputHandler.OnFire += HandleAttack;
        }

        /// <summary>
        /// Sets the PlayerHandlers index.
        /// </summary>
        /// <param name="myIndex">lbs naming convention btw</param>
        public void InitializeHandler(int myIndex)
        {
            Index = myIndex;
        }

        private bool ValidateInputPacket(PlayerInputPacket packet)
        {
            return packet.PlayerIndex.Equals(Index);
        }

        /// <summary>
        /// Applies specified damage to the player.
        /// Applies knockback effect to the player.
        /// </summary>
        /// <param name="spec">
        /// Attack spec applied to the player.
        /// </param>
        /// <param name="collisionPoint">
        /// Vector2 of position where another player collided with the target.
        /// Can be default if damage wasn't applied by a player.
        /// </param>
        public void ApplyDamage(Attack spec, Vector2 direction, Vector2 collisionPoint = default)
        {
            myHealth.ApplyDamage(spec, direction, collisionPoint);
            OnKnockbackUpdate?.Invoke(Index, myHealth.KnockbackPercentage);
        }

        private void HandleMove(PlayerInputPacket packet)
        {
            if(!ValidateInputPacket(packet)) return;

            if(packet.PlayerIndex != Index) return;
            myMovement.HandleMove(packet);
        }

        private void HandleAttack (PlayerInputPacket packet, AttackTypes type)
        {
            if(!ValidateInputPacket(packet)) return;

            Tuple<Attack, Vector2, RaycastHit2D> attackInfo = myAttack.Attack((int) type);
            if(attackInfo == default) return;

            (Attack attackSpec, Vector2 direction, RaycastHit2D hit) = attackInfo;
            mySoundSource.PlayPunchEffect();
            OnAttack?.Invoke(hit, direction, attackSpec);
        }

      private void HandleGameOver()
      {
          transform.position = new Vector3(0,0,0);
          GetComponent<SpriteRenderer>().enabled = false;
          GetComponent<Rigidbody2D>().simulated = false;
          GetComponent<Collider2D>().enabled = false;
          StartCoroutine(GetComponent<PlayerMovement>().Stun(69));
          OnGameOver?.Invoke(Index);
      }  
    }
}
