using System;

namespace Player
{
    [Serializable]
    public struct TargetedAnimations
    {
        [SerializeField] public AnimationClip Kick;
        [SerializeField] public AnimationClip Punch;
        [SerializeField] public AnimationClip Jump;
        [SerializeField] public AnimationClip Move;
        [SerializeField] public AnimationClip Hit;
        [SerializeField] public AnimationClip Idle;
    }
}
