using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerHitDetection))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerHandler : MonoBehaviour
    {
        private PlayerHealth myHealth;
        private PlayerAttack myAttack;
        private PlayerMovement myMovement;
        private PlayerInputHandler myInputHandler;
        private PlayerHitDetection myHitDetection;

        public int Index { get; private set; }
        public float Health {
            get { return myHealth.Health; }
        }

        private void Start() {
            myInputHandler = GetComponent<PlayerInputHandler>();
            myHitDetection = GetComponent<PlayerHitDetection>();
            myMovement = GetComponent<PlayerMovement>();
            myHealth = GetComponent<PlayerHealth>();

            myInputHandler.OnMove += HandleMove;
            myInputHandler.OnFire += HandleAttack;
            myHitDetection.OnPlayerHit += HandleHit;
        }

        /// <summary>
        /// Sets the PlayerHandlers index.
        /// </summary>
        /// <param name="myIndex">lbs naming convention btw</param>
        public void InitializeHandler(int myIndex)
        {
            Index = myIndex;
        }

        /// <summary>
        /// Applies specified damage to the player.
        /// Applies knockback effect to the player.
        /// </summary>
        /// <param name="myIndex">
        /// Damage points that will be applied to the player
        /// </param>
        /// <param name="collisionPoint">
        /// Vector2 of position where another player collided with the target.
        /// Can be default if damage wasn't applied by a player.
        /// </param>
        public void ApplyDamage(float damage, Vector2 collisionPoint = default)
        {
            myHealth.ApplyDamage(damage, collisionPoint);
        }

        private void HandleMove(PlayerInputPacket packet)
        {
            if(packet.PlayerIndex != Index) return;
            myMovement.HandleMove(packet);
        }

        private void HandleHit(Vector2 collisionPoint)
        {
            ApplyDamage(10f, collisionPoint);
        }

        private void HandleAttack ()
        {
            Vector2 collisionPoint = myAttack.AttackForward();
            print(collisionPoint);
            if(collisionPoint == default) return;
        }
    }
}
