using System.Collections;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class JumpBoostEffect: IPowerup
    {
        private const float JumpModifier = 2.0f;
        private const int ActiveTime = 30;
        
        private bool myIsActive = true;
        
        private PlayerMovement myImpactedMovement;

        // TODO: Probably needs a revisit when its time for online multiplayer.
        public IEnumerator ScheduleEffect(GameObject player)
        {
            myImpactedMovement = player.GetComponent<PlayerMovement>();
            if (myIsActive)
                myImpactedMovement.AddToJumpHeight(JumpModifier);
            yield return new WaitForSeconds(ActiveTime);
            CleanUp();
        }


        public void CleanUp()
        {
            if (myIsActive)
            {
                myIsActive = false;
                myImpactedMovement.AddToJumpHeight(JumpModifier * -1);
            }

        }
    }
}
