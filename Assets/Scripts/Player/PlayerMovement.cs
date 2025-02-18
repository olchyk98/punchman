﻿using System.Collections;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PlayerAnimations))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D myRb;
        private PlayerInputHandler myInputHandler;
        private Transform myTransform;
        private SpriteRenderer myRenderer;
        private PlayerAnimations myAnimations;
        private bool isStunned;
        
        [SerializeField] private PlayerGroundCheck myGroundCheck;
        [SerializeField] [Range(5f, 20f)] private float myJumpHeight;
        [SerializeField] [Range(6f, 15f)] private float mySpeed;

        private bool hasStopped;

        private void Start()
        {
            myRb = GetComponent<Rigidbody2D>();
            myTransform = GetComponent<Transform>();
            myInputHandler = GetComponent<PlayerInputHandler>();
            myRenderer = GetComponent<SpriteRenderer>();
            myAnimations = GetComponent<PlayerAnimations>();
        }

        private void FixedUpdate()
        {
            if(myAnimations.Animator == default) return;

            if(!hasStopped)
            {
                hasStopped = true;
            } else
            {
                myAnimations.Animator.SetBool(
                    "isMoving",
                    false
                );
            }
        }

        private void OnDestroy() {
            myInputHandler.OnMove -= HandleMove;
        }

        private void Update() {
            // Faster falling
            if (myRb.velocity.y < 0) {
                myRb.velocity += Vector2.up * (Physics.gravity.y * (.4f - 1) * Time.deltaTime);
            }
        }

        public void HandleMove(PlayerInputPacket packet)
        {
            if (isStunned) return;
            
            // Apply movement force
            var direction = packet.MovementDirection;

            // Jump
            // and fly down
            if((direction.y > 0f && myGroundCheck.isTouchingGround)
                    || (direction.y < 0f && !myGroundCheck.isTouchingGround))
            {
                myRb.AddForce(new Vector2(0, myJumpHeight * direction.y), ForceMode2D.Impulse);

                if(direction.y > 0)
                {
                    myAnimations.PlayAnimation("Jump");
                }
            }

            // Handle x axis
            Vector2 currentPosition = myTransform.position;
            currentPosition.x += direction.x * (Time.deltaTime * mySpeed);
            myTransform.position = currentPosition;

            // Flip model
            HandleRotation(direction.x);

            // Handle Animation
            myAnimations.Animator.SetBool(
                "isMoving",
                true
            );


            hasStopped = false;
        }

        private void HandleRotation(float x)
        {
            if(x == 0f) return;

            myTransform.rotation = x < 0
                ? Quaternion.Euler(0, 180, 0)
                : Quaternion.identity;
        }

        /// <summary>
        /// Adds the given amount to the players current max jump height
        /// </summary>
        /// <param name="jumpModifier">The amount to modify the jump height with</param>
        public void AddToJumpHeight(float jumpModifier)
        {
            myJumpHeight += jumpModifier;
        }

        /// <summary>
        /// Adds the given amount to the players speed
        /// </summary>
        /// <param name="speedModifier">The amount to modify the speed with</param>
        public void AddToSpeed(float speedModifier)
        {
            mySpeed += speedModifier;
        }

        public IEnumerator Stun(float aStunTime)
        {
            isStunned = true;
            yield return new WaitForSeconds(aStunTime);
            isStunned = false;
        }
    }
}
