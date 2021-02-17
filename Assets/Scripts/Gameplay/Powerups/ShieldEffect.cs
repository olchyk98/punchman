using System.Collections;
using Gameplay.Materials;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    public class ShieldEffect : IPowerup
    {
        // look the way i get this is ugly, ik please dont kill me ok : ).
        // The obvious answer is just to make it adapt to MonoBehaviour, but that has the caveat of it being required to be in a gameobject.
        // And we dont want a new component. And while yes we could do it without a component, that will only work in Edit Mode. It WILL silently fail out when the game is built.
        private GameObject myMainGlowingObject = PrefabManager.Main.GetPrefabByName("Shield Glow");
        private const int ActiveTime = 30;

        private bool myIsActive = true;
        private PlayerHealth myPlayerHealth;
        private GameObject myInstantiatedGlow; // fucking kill me
        private PlayerPowerup myPowerup;
        
        
        public IEnumerator ScheduleEffect(GameObject player)
        {
            myPlayerHealth = player.GetComponent<PlayerHealth>();
            myPowerup = player.GetComponent<PlayerPowerup>();
            if (myIsActive)
            {
                // fucking kill me.
                myInstantiatedGlow = myPowerup.InstantiateEffectPrefabFromPerspectiveOfPlayer(myMainGlowingObject);
                myPlayerHealth.ShieldUsed += CleanUp;
                myPlayerHealth.hasShield = true;
            }
            yield return new WaitForSeconds(ActiveTime);
            CleanUp();
        }
        
        public void CleanUp()
        {
            if (!myIsActive) return;
            myIsActive = false;
            myPowerup.DestroyEffectPrefabFromPerspectiveOfPlayer(myInstantiatedGlow);
            myPlayerHealth.hasShield = false;
        }
    }
}