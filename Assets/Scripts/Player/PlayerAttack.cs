using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Collider2D))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private List<Attack> attacks;
        private Transform myTransform;
        private bool onCooldown;

        private void Start()
        {
            myTransform = GetComponent<Transform>();
        }

        public RaycastHit2D AttackForward ()
        {
            List<RaycastHit2D> hits = (Physics2D.RaycastAll(
                myTransform.position,
                Vector2.right
            )).ToList();

            // Get first player element that's not ourselves
            Predicate<RaycastHit2D> targetPredicate = (f) => (f.collider.gameObject.CompareTag("Player")
                    && !GameObject.ReferenceEquals(gameObject, f.collider.gameObject));

            RaycastHit2D hit = hits.Find(targetPredicate);

            if(hit == default) return default;

            return hit;
        }

        public void RunAttack(int i)
        {
            if (i < attacks.Count && i >= 0)
            {

            }
            else
            {
                Debug.LogError(i + " is out of bounds in HitDetection script of " + gameObject.name); 
            }
        }
    }
}
