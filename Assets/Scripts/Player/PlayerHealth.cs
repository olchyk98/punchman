using UnityEngine;

namespace Player {
    public class PlayerHealth : MonoBehaviour
    {
        private Rigidbody2D myRb;

        public static float MaxKnockbackForce = 0f;
        public static float MaxHealth = 100f;

        public float Health { get; private set; }

        private void Start() {
            myRb = GetComponent<Rigidbody2D>();
            Health = 1f;
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
        public void ApplyDamage(float damage, Vector2 collisionPoint = default) {
            Health -= damage;
            if(collisionPoint != default)
            {
                KnockBody(damage, collisionPoint);
            }
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
        private void KnockBody(float damage, Vector2 collisionPoint) {
            // Calculate force
            // (1-Health/MaxHealth)*MaxKnockbackForce
            float force = (1 - Health / MaxHealth) * MaxKnockbackForce;
            Vector2 forceVector = new Vector2(force, force);

            // Apply relative force
            myRb.AddForceAtPosition(forceVector, collisionPoint, ForceMode2D.Impulse);
        }

    }
}
