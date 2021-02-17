using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public struct Animations
    {
        [SerializeField] public AnimationClip Kick;
        [SerializeField] public AnimationClip Punch;
        [SerializeField] public AnimationClip Jump;
        [SerializeField] public AnimationClip Move;
        [SerializeField] public AnimationClip Hit;
        [SerializeField] public AnimationClip Idle;
    }
}
