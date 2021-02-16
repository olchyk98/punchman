using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        public Animator Animator { get; private set; } = default;

        private void Start()
        {
            Animator = GetComponent<Animator>();
        }

        public void PlayAnimation(string label)
        {
            Animator.SetTrigger(label);
        }
    }

}
