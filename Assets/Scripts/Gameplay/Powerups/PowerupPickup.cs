using System;
using System.Linq;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Powerups
{
    
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(ParticleSystem))]
    
    public class PowerupPickup : MonoBehaviour
    {

        public UnityAction OnPickUp;
        
        private PowerupTypes myPowerupType;

        [SerializeField] private GameObject myAudioController;
        [SerializeField] private PowerupMetadata[] myPowerupMetas;
        public PowerupMetadata GetPowerupMetadataWithType(PowerupTypes type)
        {
            return myPowerupMetas.First(m => m.Type == type);
        }
        


        #region power up getters

        static IPowerup GetPowerUpFromType(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return new JumpBoostEffect();
                case PowerupTypes.SPEED_BOOST:
                    return new SpeedBoostEffect();
                case PowerupTypes.DAMAGE_BOOST:
                    return new DamageBoostEffect();
            }
            throw new NotImplementedException("There is no powerup for this yet lmao");
        }
        
        Color GetColorFromPowerUp(PowerupTypes type)
        {
            return GetPowerupMetadataWithType(type).Color;
        }
        #endregion
        
        
        /// <summary>
        /// Sets the type of the powerup, must be done after instantiated because unity is special.
        /// </summary>
        /// <param name="aType">The powerup type.</param>
        public void SetPowerUpType(PowerupTypes aType)
        {
            myPowerupType = aType;
            SetPowerUpColor(GetColorFromPowerUp(myPowerupType));
            gameObject.SetActive(true);
        }

        private void SetPowerUpColor(Color color)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var powerupComponent = other.gameObject.GetComponent<PlayerPowerup>();
            // Check that the other colliding GameObject is a player.
            if (powerupComponent == null) return;
            IPowerup powerup = GetPowerUpFromType(myPowerupType);
            // Remove the effects of any previously held powerup.
            powerupComponent.CancelEffects();
            // Apply the new powerup
            powerupComponent.ApplyPowerup(powerup);
            // Play the audio for the power up
            GameObject audio = Instantiate(myAudioController);
            var powerupAudio = GetPowerupMetadataWithType(myPowerupType).Audio;
            audio.GetComponent<PowerupSoundPlayback>().SetAudio(powerupAudio);
            // Tell the powerup spawner that its no longer there.
            OnPickUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
