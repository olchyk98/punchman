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
        [SerializeField] private List<Attack> attacks;
        private List<int> myAttackAnimations = new List<int>();
        private int myCooldown;

        private Transform myTransform;
        private Animator myAnimator;
        private bool onCooldown;

        private void Start()
        {
            myTransform = GetComponent<Transform>();
            
            // Get all the hashes for faster animation calls
            foreach (var aAttack in attacks)
            {
                myAttackAnimations.Add(Animator.StringToHash(aAttack.playerAnimationTrigger));
            }
        }

        public RaycastHit2D AttackForward ()
        {
            List<RaycastHit2D> hits = (Physics2D.RaycastAll(
                myTransform.position,
                Vector2.right
            )).ToList();

            // Get first player element that's not ourselves
            Predicate<RaycastHit2D> targetPredicate = (f) => (f.collider.gameObject.CompareTag("Player")
                    && !ReferenceEquals(gameObject, f.collider.gameObject));

            RaycastHit2D hit = hits.Find(targetPredicate);

            if(hit == default) return default;

            return hit;
        }

        public bool RunAttack(int i)
        {
            if (i < attacks.Count && i >= 0)
            {
                // Cooldown handling
                if (CheckCooldown(i) || onCooldown) return false;
                SetCooldown(i, attacks[i].cooldown);
                onCooldown = true;
                StartCoroutine(RemoveCooldown(attacks[i].animationCooldown));

                // Attack execution
                myAnimator.SetTrigger(myAttackAnimations[i]);

                return true;
            }
            
            Debug.LogError(i + " is out of bounds in HitDetection script of " + gameObject.name);
            return false;
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
