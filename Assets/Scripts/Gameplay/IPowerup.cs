using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public interface IPowerup
    {
        /// <summary>
        /// Modifies the given players attributes in order to apply the effect
        /// </summary>
        /// <param name="player">The player that should be affected.</param>
        /// <returns>coroutine magic</returns>
        IEnumerator ScheduleEffect(GameObject player);
        
        
        /// <summary>
        /// Removes all impacts of the effect.
        /// </summary>
        void CleanUp();
    }
}