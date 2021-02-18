using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerBoundsCheck: MonoBehaviour
    {
        public UnityAction OnOutOfBounds;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.name == "Map in bounds")
            {
                OnOutOfBounds?.Invoke();
            }
        }
    }
}