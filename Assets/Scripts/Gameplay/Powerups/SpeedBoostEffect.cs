using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class SpeedBoostEffect: IPowerup
    {
        private const float SpeedModifier = 2.0f;
        private const int ActiveTime = 30;

        private PlayerMovement _movementComponent;
        
        private bool _isActive = true;
        
        
        
        public IEnumerator ScheduleEffect(GameObject player)
        {
            _movementComponent = player.GetComponent<PlayerMovement>();
            if (_isActive)
            {
                _movementComponent.AddToSpeed(SpeedModifier);
            }
            
            
            yield return new WaitForSeconds(ActiveTime);
            CleanUp();
        }

        public void CleanUp()
        {
            if (_isActive)
            {
                _isActive = false;
                _movementComponent.AddToSpeed(SpeedModifier * -1);
            }
        }
    }
}