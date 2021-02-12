using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class JumpBoostEffect: IPowerup
    {

        private const float JumpModifier = 2.0f;
        private const int ActiveTime = 30;
        
        private bool _isActive = true;
        
        private GameObject _impactedPlayer;

        // TODO: Probably needs a revisit when its time for online multiplayer.
        public IEnumerator ScheduleEffect(GameObject player)
        {
            _impactedPlayer = player;
            if (_isActive)
                _impactedPlayer.GetComponent<PlayerMovement>().AddToJumpHeight(JumpModifier);
            yield return new WaitForSeconds(ActiveTime);
            CleanUp();
        }

        
        public void CleanUp()
        {
            if (_isActive)
            {
                _isActive = false;
                _impactedPlayer?.GetComponent<PlayerMovement>().AddToJumpHeight(JumpModifier * -1);
            }
            
        }
    }
}