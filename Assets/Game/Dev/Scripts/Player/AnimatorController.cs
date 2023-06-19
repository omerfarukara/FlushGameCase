using System;
using UnityEngine;

namespace FlushGameCase.Game.Player
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private Rigidbody _rigidbody;
        private static readonly int Blend = Animator.StringToHash("Blend");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            animator.SetFloat(Blend, _rigidbody.velocity.magnitude);
        }
    }
}
