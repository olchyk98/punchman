using UnityEngine;


namespace Gameplay
{
    /// <summary>
    /// A struct that contains music track information.
    /// </summary>
    [System.Serializable]
    public struct MusicTrack
    {
        [SerializeField] public string TrackName;
        [SerializeField] public MusicType Type;
        [SerializeField] public AudioClip Audio;
    }
}