using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D myRb;
        private PlayerInputHandler myInputHandler;
        private Transform myTransform;
        private SpriteRenderer myRenderer;
        [SerializeField] private PlayerGroundCheck myGroundCheck;

        [SerializeField] [Range(5f, 20f)] private float myJumpHeight;
        [SerializeField] [Range(6f, 15f)] private float mySpeed;

        private void Start()
        {
            myRb = GetComponent<Rigidbody2D>();
            myTransform = GetComponent<Transform>();
            myInputHandler = GetComponent<PlayerInputHandler>();
            myRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDestroy() {
            myInputHandler.OnMove -= HandleMove;
        }

        private void Update() {
            // Faster falling
            if (myRb.velocity.y < 0) {
                myRb.velocity += Vector2.up * Physics.gravity.y * (.4f - 1) * Time.deltaTime;
            }
        }

        public void HandleMove(PlayerInputPacket packet)
        {
            // Apply movement force
            var direction = packet.MovementDirection;

            // Jump
            // and fly down
            if((direction.y > 0f && myGroundCheck.isTouchingGround)
                    || (direction.y < 0f && !myGroundCheck.isTouchingGround))
            {
                myRb.AddForce(new Vector2(0, myJumpHeight * direction.y), ForceMode2D.Impulse);
            }

            // Handle x axis
            Vector2 currentPosition = myTransform.position;
            currentPosition.x += direction.x * (Time.deltaTime * mySpeed);
            myTransform.position = currentPosition;

            // Flip model
            if(direction.x != 0f) {
                myRenderer.flipX = direction.x < 0f;
            }
        }

        public void AddToJumpHeight(float modifier)
        {
            myJumpHeight += modifier;
        }
    }
}
