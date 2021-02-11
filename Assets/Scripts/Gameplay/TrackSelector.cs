using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// A class that holds a list of all available music tracks.
    /// </summary>
    public class TrackSelector: MonoBehaviour
    {
        [SerializeField] private List<MusicTrack> tracks = new List<MusicTrack>();

        /// <summary>
        /// Query the internal track library to find tracks matching the given type.
        /// </summary>
        /// <param name="type">Track type</param>
        /// <returns>A list of all MusicTrack's with the same type.</returns>
        public List<MusicTrack> GetTracksByType(MusicType type)
        {
            return tracks.Where(m => m.Type == type).ToList();
        }
        
    }
}