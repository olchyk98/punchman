using UnityEngine;

namespace Gameplay.Powerups
{
    [System.Serializable]
    public struct PowerupMetadata
    {
        [SerializeField] public PowerupTypes Type;
        [SerializeField] public Color Color;
        [SerializeField] public AudioClip Audio;
    }
}