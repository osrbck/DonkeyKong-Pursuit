using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAgent : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Animator _animator;

    void Start()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    public void Move(Vector2 velocity)
    {
        if (velocity.x != 0f)
            _animator.SetBool("isRunning", true);

        else
            _animator.SetBool("isRunning", false);
    }

    public void StopAnimations()
    {
        _animator.SetBool("isRunning", false);
        _animator.playbackTime = 0f;
    }
}

