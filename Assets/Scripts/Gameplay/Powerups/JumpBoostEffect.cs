using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class JumpBoostEffect: IPowerup
    {

        private float jumpModifier = 2.0f;
        private int activeTime = 30;

        private bool isActive = true;

        private GameObject _impactedPlayer;

        // TODO: Probably needs a revisit when its time for online multiplayer.
        public IEnumerator ScheduleEffect(GameObject player)
        {
            _impactedPlayer = player;
            if (isActive)
                _impactedPlayer.GetComponent<PlayerMovement>().AddToJumpHeight(jumpModifier);
            yield return new WaitForSeconds(activeTime);
            CleanUp();
        }


        public void CleanUp()
        {
            if (isActive)
            {
                isActive = false;
                _impactedPlayer?.GetComponent<PlayerMovement>().AddToJumpHeight(jumpModifier * -1);
            }

        }
    }
}
