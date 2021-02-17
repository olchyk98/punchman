using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public struct Attack
    {
        public Vector2 direction;
        [Range(.1f, 3f)]
        public float affection;
        public float cooldown;
        public float animationCooldown;
        public string animationTriggerName;
        [Range(.1f, 50f)]
        public int maxDistance;
    }
}
