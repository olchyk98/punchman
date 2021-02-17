using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : MonoBehaviour
    {
        private Transform myTransform;
        private Animator myAnimator;

        [SerializeField] private List<Attack> attacks;
        private List<int> myAttackAnimations = new List<int>();

        private int myCooldown;
        private bool onCooldown;

        private void Start()
        {
            myTransform = GetComponent<Transform>();
            myAnimator = GetComponent<Animator>();

            // Get all the hashes for faster animation calls
            foreach (var attackSpec in attacks)
            {
                var nameHash = Animator.StringToHash(
                    attackSpec.animationTriggerName
                );


                myAttackAnimations.Add(nameHash);
            }
        }

        public Tuple<Attack, RaycastHit2D> Attack (int attackIndex)
        {
            if(!RunAttack(attackIndex)) return default;

            var attackSpec = attacks[attackIndex];
            var actorX = myTransform.right.x;

            List<RaycastHit2D> hits = (Physics2D.RaycastAll(
                myTransform.position,
                Vector2.right * actorX,
                distance: attackSpec.maxDistance
            )).ToList();

            // Get first player element that's not ourselves
            Predicate<RaycastHit2D> targetPredicate = (f) => {
                var targetObj = f.collider.gameObject;

                return targetObj.CompareTag("Player")
                    && !ReferenceEquals(gameObject, targetObj);
            };

            RaycastHit2D hit = hits.Find(targetPredicate);

            if(hit == default) return default;

            return Tuple.Create(attackSpec, hit);
        }

        private void ClearTriggers()
        {
            foreach (var hash in myAttackAnimations)
            {
                myAnimator.ResetTrigger(hash);
            }
        }

        /// <summary>
        /// Checks the cooldown and runs attack animation.
        /// <param name="i">Index of the targeted attack spec</param>
        /// </summary>
        /// <returns>Boolean that represents if player can use this attack</returns>
        private bool RunAttack(int i)
        {
            if (i > attacks.Count || i < 0)
            {
                Debug.LogError(i + " is out of bounds in HitDetection script of " + gameObject.name);
                return default;
            }

            // Cooldown handling
            if (CheckCooldown(i) || onCooldown) return false;

            ClearTriggers();
            SetCooldown(i, attacks[i].cooldown);
            onCooldown = true;
            StartCoroutine(RemoveCooldown(attacks[i].animationCooldown));

            // Attack execution
            myAnimator.SetTrigger(myAttackAnimations[i]);

            return true;
        }

        #region Cooldown methods

        /// <summary>
        /// Sets the cooldown at a specific index.
        /// </summary>
        /// <param name="i">index</param>
        /// <param name="aCooldown">Cooldown timer in seconds</param>
        private void SetCooldown(int i, float aCooldown)
        {
            if (CheckCooldown(i)) return;
            myCooldown += 1 << i;
            StartCoroutine(RemoveCooldown(aCooldown, i));
        }

        /// <summary>
        /// Removes the cooldown after <paramref name="aCooldown"/> seconds have passed
        /// </summary>
        /// <param name="i">index to remove cooldown from</param>
        /// <param name="aCooldown">Cooldown timer in seconds</param>
        private IEnumerator RemoveCooldown(float aCooldown, int i = -1)
        {
            yield return new WaitForSeconds(aCooldown);
            if (i == -1)
                onCooldown = false;
            else if (CheckCooldown(i))
            {
                myAnimator.ResetTrigger(myAttackAnimations[i]);
                myCooldown -= 1 << i;
            }
        }

        /// <summary>
        /// Checks whether or not the specific index of an attack is on cooldown
        /// 00001111 / 15
        /// ^^ that would imply that index 0-3 is on cooldown
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>Whether or not the specific index is on cooldown or not</returns>
        private bool CheckCooldown(int i)
        {
            return ((myCooldown >> i) & 1) == 1;
        }

        #endregion
    }
}
