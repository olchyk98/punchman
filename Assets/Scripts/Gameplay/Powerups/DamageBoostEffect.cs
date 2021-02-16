using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class DamageBoostEffect : IPowerup
    {
        private const int ActiveTime = 30;
        private bool myIsActive = true;
        private PlayerHandler myPlayerHandler;
        private float myAmplifier = 0.25f;
        
        public IEnumerator ScheduleEffect(GameObject player)
        {
            if (myIsActive)
            {
                myPlayerHandler = player.GetComponent<PlayerHandler>();
                myPlayerHandler.myAmplificationPercentage += myAmplifier;
            }
            yield return new WaitForSeconds(ActiveTime);
            CleanUp();
        }

        public void CleanUp()
        {
            if (!myIsActive) return;
            myIsActive = false;
            myPlayerHandler.myAmplificationPercentage -= myAmplifier;
        }
    }
}