// TODO: REMOVE AS IT'S DEPRECATED
using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerHitDetection : MonoBehaviour
    {
        private Transform myTransform;
        public UnityAction<Vector2> OnPlayerHit;

        private void Start()
        {
            myTransform = GetComponent<Transform>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag != "Player") return;
            return;

            var otherDirection = (other.gameObject.transform.position - myTransform.position);
            var contactPoint = myTransform.position + otherDirection;

            OnPlayerHit?.Invoke(contactPoint);
        }
    }
}
