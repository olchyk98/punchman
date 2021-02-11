using System;
using Player;
using UnityEngine;

namespace Gameplay.Powerups
{
    
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(ParticleSystem))]
    
    public class PowerupPickup : MonoBehaviour
    {
        
        private PowerupTypes _powerupType;

        [SerializeField] private GameObject audioController;
        


        #region power up getters

        static IPowerup GetPowerUpFromType(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return new JumpBoostEffect();
            }
            throw new NotImplementedException("There is no powerup for this yet lmao");
        }
        
        static Color GetColorFromPowerUp(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return Color.cyan;
            }
            throw new NotImplementedException("this color does not exist.");
        }
        #endregion
        
        
        /// <summary>
        /// Sets the type of the powerup, must be done before instantiated so it's set before start.
        /// </summary>
        /// <param name="aType">The powerup type.</param>
        public void SetPowerUpType(PowerupTypes aType)
        {
            _powerupType = aType;
        }

        private void SetPowerUpColor(Color color)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        private void Start()
        {
            SetPowerUpColor(GetColorFromPowerUp(_powerupType));
            audioController.GetComponent<PowerupSoundPlayback>().SetType(_powerupType);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check that the other colliding GameObject is a player.
            if (other.gameObject.GetComponent<PlayerHandler>() == null)
                return;
            IPowerup powerup = GetPowerUpFromType(_powerupType);
            StartCoroutine(powerup.ScheduleEffect(other.gameObject));
            Instantiate(audioController);
            Destroy(gameObject);
        }
    }
}
