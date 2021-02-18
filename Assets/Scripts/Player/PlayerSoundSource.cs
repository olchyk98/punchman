using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSoundSource: MonoBehaviour
    {
        [SerializeField] private AudioClip[] myPunchEffects;
        
        private AudioSource myAudioSource;

        private void Start()
        {
            myAudioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Selects a random punch effect and initiates playback.
        /// </summary>
        public void PlayPunchEffect()
        {
            int audioIndex = Random.Range(0, myPunchEffects.Length);
            myAudioSource.clip = myPunchEffects[audioIndex];
            myAudioSource.Play();
        }

    }
}