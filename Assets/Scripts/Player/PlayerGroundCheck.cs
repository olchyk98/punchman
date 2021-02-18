using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerGroundCheck : MonoBehaviour
{
    public bool isTouchingGround { get; private set; } = false;

    private void Start() {
        GetComponent<BoxCollider2D>()
            .isTrigger = true;
    }

    // Looks like we also need this in order to prevent race-conditions, very cool Unity!
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.isTrigger) return;
        isTouchingGround = true;
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.isTrigger) return;
        isTouchingGround = false;
    }
}
