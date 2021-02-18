using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float stunTime = 0.5f;
        [SerializeField] private GameObject attackSplashPrefab;

        private Rigidbody2D myRb;
        private Transform myTransform;
        private PlayerMovement myMovement;
        private PlayerAttack myAttack;

        public static float MaxKnockbackForce = 100f;
        public static float MaxHealth = 100f;

        public bool hasShield;
        public UnityAction ShieldUsed;

        private const float myRegularKnockback = 15f;
        public float KnockbackPercentage = 1f;

        private void Start() {
            myRb = GetComponent<Rigidbody2D>();
            myTransform = GetComponent<Transform>();
            myMovement = GetComponent<PlayerMovement>();
            myAttack = GetComponent<PlayerAttack>();
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
            KnockbackPercentage += spec.affection;

            if (hasShield)
            {
                ShieldUsed?.Invoke();
                hasShield = false;
                return;
            }

            if(collisionPoint != default)
            {
                KnockBody(spec, direction, collisionPoint);
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
        private void KnockBody(Attack spec, Vector2 direction, Vector2 collisionPoint)
        {
            StartCoroutine(myAttack.Stun(stunTime));
            StartCoroutine(myMovement.Stun(stunTime));

            // Construct force vector
            var position = myTransform.position;

            float forceX = (spec.direction.x == 0f)
                ? 0
                : position.x > collisionPoint.x ? 1 : -1;

            var forceVector = new Vector2(forceX * direction.x, direction.y) * (KnockbackPercentage * myRegularKnockback);
            myRb.AddForce(forceVector, ForceMode2D.Impulse);

            // Spawn splash effect object
            Instantiate(attackSplashPrefab, collisionPoint, Quaternion.identity);

            // Shake Camera
            ApplyHitToCamera();
        }

        /// <summary>
        /// Makes camera shake in respective to the current
        /// knockback percentage.
        /// </summary>
        private void ApplyHitToCamera()
        {
            const float maxForce = 5f;
            var force = Mathf.Clamp(KnockbackPercentage, 0f, maxForce);
            Shake.AddTrauma(force / maxForce * 10f);
        }
    }
}
