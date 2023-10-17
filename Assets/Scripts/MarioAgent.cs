using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class MarioAgent : MonoBehaviour
    {
        [SerializeField] private MarioController _marioCtrl;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        
        public void RunningAnimate(Vector2 velocity)
        {
            if (velocity.x < 0f)
                _spriteRenderer.flipX = true;

            else
                _spriteRenderer.flipX = false;

            if (velocity.x != 0f)
                _animator.SetBool("isRunning", true);

            else
                _animator.SetBool("isRunning", false);
        }


        public void ClimbingAnimate(Vector2 velocity)
        {
            if (velocity.y != 0f)
                _animator.SetBool("isClimbing", true);

            else
            {
                _animator.SetBool("isClimbing", false);
                _animator.SetTrigger("Climb");
            }
                

        }

        public void StopAnimations()
        {
            _animator.SetBool("isClimbing", false);
            _animator.SetBool("isRunning", false);
            _animator.playbackTime = 0f;
        }

    }
}