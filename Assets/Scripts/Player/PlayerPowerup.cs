using Gameplay.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerPowerup : MonoBehaviour
    {

        private IPowerup _activePowerUp;

        public void ApplyPowerup(IPowerup powerup)
        {
            _activePowerUp = powerup;
            StartCoroutine(_activePowerUp.ScheduleEffect(gameObject));
        }

        public void CancelEffects()
        {
            _activePowerUp?.CleanUp();
        }
    }
}
