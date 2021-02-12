using System;
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

        public UnityAction DidPickUp;
        
        private PowerupTypes _powerupType;

        [SerializeField] private GameObject audioController;
        


        #region power up getters

        static IPowerup GetPowerUpFromType(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return new JumpBoostEffect();
                case PowerupTypes.SPEED_BOOST:
                    return new SpeedBoostEffect();
            }
            throw new NotImplementedException("There is no powerup for this yet lmao");
        }
        
        static Color GetColorFromPowerUp(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return Color.cyan;
                case PowerupTypes.SPEED_BOOST:
                    return Color.green;
            }
            throw new NotImplementedException("this color does not exist.");
        }
        #endregion
        
        
        /// <summary>
        /// Sets the type of the powerup, must be done after instantiated because unity is special.
        /// </summary>
        /// <param name="aType">The powerup type.</param>
        public void SetPowerUpType(PowerupTypes aType)
        {
            _powerupType = aType;
            SetPowerUpColor(GetColorFromPowerUp(_powerupType));
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
            if (powerupComponent == null)
                return;
            IPowerup powerup = GetPowerUpFromType(_powerupType);
            // Remove the effects of any previously held powerup.
            powerupComponent.CancelEffects();
            // Apply the new powerup
            powerupComponent.ApplyPowerup(powerup);
            // Play the audio for the power up
            GameObject audio = Instantiate(audioController);
            audio.GetComponent<PowerupSoundPlayback>().SetType(_powerupType);
            // Tell the powerup spawner that its no longer there.
            DidPickUp?.Invoke();
            // Remove itself.
            Destroy(gameObject);
        }
    }
}
