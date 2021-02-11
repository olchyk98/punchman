using System;
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

        public Vector2 AttackForward ()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                myTransform.position,
                Vector2.right
            );
            if(hit.collider == null) return default;

            return hit.point;
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
