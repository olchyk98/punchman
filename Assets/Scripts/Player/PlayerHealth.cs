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

        public float Health { get; private set; }

        private void Start() {
            myRb = GetComponent<Rigidbody2D>();
            myTransform = GetComponent<Transform>();

            Health = MaxHealth;
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
            if (hasShield)
            {
                ShieldUsed?.Invoke();
                hasShield = false;
                return;
            }
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
        private void KnockBody(float damage, Vector2 collisionPoint)
        {
            // Calculate force
            float force = (1 - Health / MaxHealth) * MaxKnockbackForce;

            // Construct force vector
            // TODO: Simplify
            var position = myTransform.position;
            float forceX = position.x > collisionPoint.x ? 1 : -1;

            float forceY = position.y > collisionPoint.y ? -1 : 1;

            var forceVector = new Vector2(forceX, forceY) * force;

            // Apply relative force
            myRb.AddForce(forceVector, ForceMode2D.Impulse);
        }
    }
}
