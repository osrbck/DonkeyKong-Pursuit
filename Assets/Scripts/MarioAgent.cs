using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit {
    public class MarioAgent : MonoBehaviour
    {
        private MarioController _marioCtrl;
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Animator _animator;

        void Start()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_marioCtrl == null)
                _marioCtrl = GetComponent<MarioController>();

            if(_animator == null)
                _animator = GetComponent<Animator>();
        }

        //public void Jump()
        //{
        //    _animator.SetTrigger("Jump");
        //}

        public void Move(Vector2 velocity)
        {
            if (velocity.x != 0f)
                _animator.SetBool("isRunning", true);

            else
                _animator.SetBool("isRunning", false);

            if (_marioCtrl.Climbing)
                _animator.SetBool("isClimbing", true);
            else
                _animator.SetBool("isClimbing", false);
        }

        public void StopAnimations()
        {
            _animator.SetBool("isRunning", false);
            _animator.playbackTime = 0f;
        }
    }

}