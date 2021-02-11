using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Music
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(TrackSelector))]
    
    public class MusicManager : MonoBehaviour
    {

        public static MusicManager Main;
        
        private AudioSource _playbackSource;
        private TrackSelector _trackLibrary;

        /// <summary>
        /// This Awake preserves the GameObject across scenes.
        /// </summary>
        private void Awake()
        {
            if(Main != null && Main != this) {
                DestroyImmediate(gameObject);
                return;
            }
            Main = this;
            DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            _playbackSource = GetComponent<AudioSource>();
            _trackLibrary = GetComponent<TrackSelector>();
        }

        
        /// <summary>
        /// Start playback of a random music track that has said track type.
        /// </summary>
        /// <param name="type">Type of track to play.</param>
        public void PlayRandomTrackOfType(MusicType type)
        {
            var applicableTracks = _trackLibrary.GetTracksByType(type);
            if (applicableTracks.Count == 0)
            {
                Debug.LogError("Unable to proceed and play an applicable music track since there are no tracks available of that type.");
                return;
            }

            var randomTrack = applicableTracks[Random.Range(0, applicableTracks.Count - 1)];
            PlayTrack(randomTrack);

        }

        /// <summary>
        /// Internal method thats used to start playback of a given MusicTrack
        /// </summary>
        /// <param name="track">What track to play.</param>
        /// <returns>Coroutine magic.</returns>
        private void PlayTrack(MusicTrack track)
        {
            _playbackSource.clip = track.Audio;
            _playbackSource.Play();
        }
        
    }
}
