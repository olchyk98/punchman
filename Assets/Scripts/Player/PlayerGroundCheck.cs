using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerGroundCheck : MonoBehaviour
{
    public bool isTouchingGround { get; private set; } = false;

    private void Start() {
        GetComponent<BoxCollider2D>()
            .isTrigger = true;
    }

    private void OnTriggerEnter2D() {
        isTouchingGround = true;
    }

    private void OnTriggerExit2D() {
        isTouchingGround = false;
    }
}
