using System;
using System.Collections;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Gameplay.Powerups
{
    public class PowerupSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject powerupPickUp;
        [SerializeField] [Range(10f, 120f)] private float baseRespawnDelay;
        [SerializeField] [Range(5f, 10f)] private float maxRandomExtraRespawnDelay;
        [SerializeField] private PowerupTypes[] spawnTypes;
        [SerializeField] private int spawnHeightAboveSpawner;
        
        private Vector3 _spawnerPosition;
        private bool _holdingSpawnedPowerup;
        private Random _rng;

        // Start is called before the first frame update
        private void Start()
        {
            // For some reason if I dont explicitly set a seed it errors out, very cool unity
            _rng = new Random((uint)DateTime.Now.Millisecond);
            var location = gameObject.transform.position;
            location.y += spawnHeightAboveSpawner;
            _spawnerPosition = location;
            StartCoroutine(StartSpawningPowerups());
        }


        /// <summary>
        /// Calculate when the spawner should run its next spawn powerup check.
        /// </summary>
        /// <returns>A random float which represents the amount of time, in seconds, that the respawner should wait before checking if its met the respawning conditions.</returns>
        private float CalculateNextSpawnCheckTime()
        {
            return baseRespawnDelay + _rng.NextFloat(0f, maxRandomExtraRespawnDelay);
        }
        
        /// <summary>
        /// Makes a random selection of the power ups that this spawner is able to create.
        /// </summary>
        /// <returns>A random power up type.</returns>
        private PowerupTypes MakeRandomPowerupSelection()
        {
            var powerupId = _rng.NextInt(0, spawnTypes.Length - 1);
            return spawnTypes[powerupId];
        }

        /// <summary>
        /// Coroutine which starts the process of spawning powerups
        /// </summary>
        /// <returns>Coroutine magic</returns>
        IEnumerator StartSpawningPowerups()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                // There is no power up currently in the spawner area, lets toss one in there.
                if (!_holdingSpawnedPowerup)
                {
                    _holdingSpawnedPowerup = true;
                    GameObject powerup = Instantiate(powerupPickUp, _spawnerPosition, Quaternion.identity);
                    var powerupComponent = powerup.GetComponent<PowerupPickup>();
                    powerupComponent.SetPowerUpType(MakeRandomPowerupSelection());
                    powerupComponent.DidPickUp += HandlePickUp;
                    yield return new WaitForSeconds(CalculateNextSpawnCheckTime());
                }
            }
        }

        /// <summary>
        /// Sent to spawned powerups as an action thats invoked to indicate when the spawned power up has been consumed.
        /// </summary>
        private void HandlePickUp()
        {
            _holdingSpawnedPowerup = false;
        }
    }
}
