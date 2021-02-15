using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class JumpBoostEffect : IPowerup
    {
        private const float myJumpModifier = 2.0f;
        private const int myActiveTime = 30;
        
        private bool myIsActive = true;
        
        private PlayerMovement myImpactedMovement;

        // TODO: Probably needs a revisit when its time for online multiplayer.
        public IEnumerator ScheduleEffect(GameObject player)
        {
            myImpactedMovement = player.GetComponent<PlayerMovement>();
            if (myIsActive)
            {
                myImpactedMovement.AddToJumpHeight(myJumpModifier);
            }
            yield return new WaitForSeconds(myActiveTime);
            CleanUp();
        }


        public void CleanUp()
        {
            if (!myIsActive) return;
            myIsActive = false;
            myImpactedMovement.AddToJumpHeight(-myJumpModifier);
        }
    }
}
