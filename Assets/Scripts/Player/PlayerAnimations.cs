using UnityEngine;

namespace Player
{
    [System.Serializable]
    internal struct TargetedAnimations
    {
        [SerializeField] public AnimationClip Kick;
        [SerializeField] public AnimationClip Punch;
        [SerializeField] public AnimationClip Jump;
        [SerializeField] public AnimationClip Move;
        [SerializeField] public AnimationClip Hit;
        [SerializeField] public AnimationClip Idle;
    }

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        public Animator Animator { get; private set; } = default;
        private AnimatorOverrideController myAnimatorOverrider;

        [SerializeField] private TargetedAnimations myAnimations;

        private void Start()
        {
            Animator = GetComponent<Animator>();
            myAnimatorOverrider = new AnimatorOverrideController(Animator.runtimeAnimatorController);

            AssignClips();
        }

        private void AssignClips()
        {
            myAnimatorOverrider["Kick"] = myAnimations.Kick;
            myAnimatorOverrider["Punch"] = myAnimations.Punch;
            myAnimatorOverrider["Jump"] = myAnimations.Jump;
            myAnimatorOverrider["Move"] = myAnimations.Move;
            myAnimatorOverrider["Hit"] = myAnimations.Hit;
            myAnimatorOverrider["Idle"] = myAnimations.Idle;
        }

        public void PlayAnimation(string label)
        {
            Animator.SetTrigger(label);
        }
    }
}
