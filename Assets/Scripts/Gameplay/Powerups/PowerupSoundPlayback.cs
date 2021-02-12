using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Powerups
{
    
    [RequireComponent(typeof(AudioSource))]
    public class PowerupSoundPlayback : MonoBehaviour
    {
        [SerializeField] private AudioClip jumpFx;
        [SerializeField] private AudioClip speedFx;

    
        #region converter
        AudioClip GetSoundFromType(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return jumpFx;
                case PowerupTypes.SPEED_BOOST:
                    return speedFx;
            }

            throw new NotImplementedException("There is no sound for this yet lmao");
        }
        #endregion

        /// <summary>
        /// Used by the pickup to tell the powerup sound prefab which sound it should select based of powerup type.
        /// </summary>
        /// <param name="aType">The powerup type</param>
        public void SetType(PowerupTypes aType)
        {
            AudioClip sfx = GetSoundFromType(aType);
            var source = GetComponent<AudioSource>();
            source.clip = sfx;
            source.Play();
            StartCoroutine(DestoryAfter(sfx.length));
        }

        /// <summary>
        /// A coroutine which destroys the GameObject holding the component after a set amount of time.
        /// </summary>
        /// <param name="time">Time in seconds.</param>
        /// <returns></returns>
        IEnumerator DestoryAfter(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
