using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerHealth : MonoBehaviour
    {
        private Rigidbody2D myRb;
        private Transform myTransform;

        public static float MaxKnockbackForce = 100f;
        public static float MaxHealth = 100f;

        public bool hasShield;
        public UnityAction ShieldUsed;

        private const float myRegularKnockback = 15f;
        public float KnockbackPercentage = 1f;

        private void Start() {
            myRb = GetComponent<Rigidbody2D>();
            myTransform = GetComponent<Transform>();
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
        public void ApplyDamage(Attack spec, Vector2 collisionPoint = default)
        {
            KnockbackPercentage += spec.affection;

            if (hasShield)
            {
                ShieldUsed?.Invoke();
                hasShield = false;
                return;
            }

            if(collisionPoint != default)
            {
                KnockBody(spec, collisionPoint);
            }
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
        private void KnockBody(Attack spec, Vector2 collisionPoint)
        {
            // Construct force vector
            var position = myTransform.position;

            float forceX = (spec.direction.x == 0f)
                ? 0
                : position.x > collisionPoint.x ? 1 : -1;

            float forceY = (spec.direction.y == 0f)
                ? 0
                : position.y > collisionPoint.y ? -1 : 1;

            var forceVector = new Vector2(forceX, forceY) * (KnockbackPercentage * myRegularKnockback);

            // Apply relative force
            myRb.AddForce(forceVector, ForceMode2D.Impulse);
        }
    }
}
