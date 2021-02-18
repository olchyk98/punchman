using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        public Animator Animator { get; private set; } = default;
        private AnimatorOverrideController myAnimatorOverrider;

        [SerializeField] private Animations myAnimations;

        private void Start()
        {
            Animator = GetComponent<Animator>();
            myAnimatorOverrider = new AnimatorOverrideController(Animator.runtimeAnimatorController);

            AssignClips();
        }

        private void AssignClips()
        {
            var animations = Animator.runtimeAnimatorController.animationClips;
            var animationsLabeled = new Dictionary<string, AnimationClip>();
            var overridedAnimations = new List<KeyValuePair<AnimationClip, AnimationClip>>();

            foreach (AnimationClip clip in animations)
            {
                animationsLabeled[clip.name] = clip;
            }

            AssignClip(animationsLabeled["Kick"], myAnimations.Kick, overridedAnimations);
            AssignClip(animationsLabeled["Punch"], myAnimations.Punch, overridedAnimations);
            AssignClip(animationsLabeled["Jump"], myAnimations.Jump, overridedAnimations);
            AssignClip(animationsLabeled["Walk"], myAnimations.Move, overridedAnimations);
            AssignClip(animationsLabeled["Hit"], myAnimations.Hit, overridedAnimations);
            AssignClip(animationsLabeled["Idle"], myAnimations.Idle, overridedAnimations);

            myAnimatorOverrider.ApplyOverrides(overridedAnimations);
            Animator.runtimeAnimatorController = myAnimatorOverrider;
        }

        private void AssignClip(
            AnimationClip previous,
            AnimationClip next,
            List<KeyValuePair<AnimationClip, AnimationClip>> store
        )
        {
            var pair = new KeyValuePair<AnimationClip, AnimationClip>(previous, next);
            store.Add(pair);
        }

        public void PlayAnimation(string label)
        {
            Animator.SetTrigger(label);
        }
    }
}
