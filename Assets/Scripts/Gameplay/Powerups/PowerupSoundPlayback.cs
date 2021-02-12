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
        

        /// <summary>
        /// Used by the pickup to tell the powerup sound prefab which sound it should select based of powerup type.
        /// </summary>
        /// <param name="aType">The powerup type</param>
        public void SetAudio(AudioClip audio)
        {
            var source = GetComponent<AudioSource>();
            source.clip = audio;
            source.Play();
            StartCoroutine(DestoryAfter(audio.length));
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
