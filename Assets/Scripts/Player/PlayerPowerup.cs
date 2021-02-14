using Gameplay.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerPowerup : MonoBehaviour
    {
        private IPowerup myActivePowerUp;

        public void ApplyPowerup(IPowerup powerup)
        {
            myActivePowerUp = powerup;
            StartCoroutine(myActivePowerUp.ScheduleEffect(gameObject));
        }

        public void CancelEffects()
        {
            myActivePowerUp?.CleanUp();
        }
    }
}
