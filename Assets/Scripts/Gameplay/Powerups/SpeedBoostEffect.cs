using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class SpeedBoostEffect: IPowerup
    {
        private const float SpeedModifier = 2.0f;
        private const int ActiveTime = 30;

        private PlayerMovement myPlayerMovement;
        private bool myIsActive = true;

        public IEnumerator ScheduleEffect(GameObject player)
        {
            myPlayerMovement = player.GetComponent<PlayerMovement>();
            if (myIsActive)
            {
                myPlayerMovement.AddToSpeed(SpeedModifier);
            }
            yield return new WaitForSeconds(ActiveTime);
            CleanUp();
        }

        public void CleanUp()
        {
            if (!myIsActive) return;
            myIsActive = false;
            myPlayerMovement.AddToSpeed(SpeedModifier * -1);
        }
    }
}