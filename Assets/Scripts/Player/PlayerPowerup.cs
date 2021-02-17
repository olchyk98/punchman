using Gameplay.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerPowerup : MonoBehaviour
    {
        private IPowerup myActivePowerUp;
        private CapsuleCollider2D myCollider;

        void Start()
        {
            myCollider = GetComponent<CapsuleCollider2D>();
        }

        public void ApplyPowerup(IPowerup powerup)
        {
            myActivePowerUp = powerup;
            StartCoroutine(myActivePowerUp.ScheduleEffect(gameObject));
        }

        public void CancelEffects()
        {
            myActivePowerUp?.CleanUp();
        }

        public GameObject InstantiateEffectPrefabFromPerspectiveOfPlayer(GameObject original)
        {
            // Gets the lowest point of the characters collider and because im brain damaged we add 0.5 to it
            Vector3 currentPosition = gameObject.transform.position;
            currentPosition.y = myCollider.bounds.min.y+0.5f;
            
            GameObject effectPrefab = Instantiate(original, currentPosition, Quaternion.identity);
            effectPrefab.transform.parent = gameObject.transform;
            return effectPrefab;
        }
        
        public void DestroyEffectPrefabFromPerspectiveOfPlayer(GameObject effectPrefab)
        {
            Destroy(effectPrefab);
        }
    }
}
