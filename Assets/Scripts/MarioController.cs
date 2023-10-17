using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class MarioController : MonoBehaviour
    {
        private MarioData _marioData;
        
        private MarioAgent _marioAgent;
        private PauseMenu _pauseMenu;

        
        private Rigidbody2D _rigidbody;
        private Collider2D _col;
        private Collider2D[] _overlaps = new Collider2D[4];
        private Vector2 _direction;

        [SerializeField] private bool _isClimbing;
        [SerializeField] private bool _isGround;


        public MarioData MarioData { get { return _marioData; } }
        public bool IsClimbing { get { return _isClimbing; } }

        void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
            if (_col == null)
                _col = GetComponent<Collider2D>();
            if (_overlaps == null)
                _overlaps = GetComponent<Collider2D[]>();

            _marioData = GetComponent<MarioData>();
            _marioAgent= GetComponent<MarioAgent>();
        }

        //private void OnEnable()
        //{
        //    InvokeRepeating(nameof(AnimateMario), 1f / 12f, 1f / 12f);
        //}

        //private void OnDisable()
        //{
        //    CancelInvoke();
        //}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.GoToMenu(MenuName.Pause);
            }
                
        }

        void FixedUpdate()
        {
            CheckCollision();
            SetDirection();
            _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Danger")
            {
                enabled = false;
                GameManager._instance.OnLevelFailed();
            }
            else if(collision.collider.tag == "Princess")
            {
                enabled = false;
                GameManager._instance.OnLevelCompleted();
            }
        }

        private void SetDirection()
        {
            if (_isClimbing)
            {
                _direction.y = Input.GetAxis("Vertical") * _marioData.ClimbSpeed * Time.deltaTime;
                _rigidbody.gravityScale = 0f;
                _marioAgent.ClimbingAnimate(_direction);
            }

            else if (_isGround && (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.W)))
            {
                _direction = Vector2.up * _marioData.JumpSpeed * Time.deltaTime;
            }

            else
            {
                _direction += Physics2D.gravity * Time.deltaTime;
            }

            _direction.x = Input.GetAxis("Horizontal") * _marioData.MoveSpeed * Time.deltaTime;
            if(_isGround)
                _marioAgent.RunningAnimate(_direction);

            // Prevent gravity from building up infinitely
            if (_isGround)
            {
                _direction.y = Mathf.Max(_direction.y, -1f);
            }
        }

        void CheckCollision()
        {
            _isGround = false;
            _isClimbing = false;

            // the amount that two colliders can overlap
            // increase this value for steeper platforms
            float skinWidth = 0.1f;
            Vector2 size = _col.bounds.size;
            size.y += skinWidth;
            size.x /= 1.5f;
            int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, _overlaps);

            for (int i = 0; i < amount; i++)
            {
                GameObject hit = _overlaps[i].gameObject;

                if (hit.layer == LayerMask.NameToLayer("Ground"))
                {
                    // Only set as grounded if the platform is below the player
                    _isGround = hit.transform.position.y < (transform.position.y - 0.5f + skinWidth);

                    // Turn off collision on platforms the player is not grounded to
                    Physics2D.IgnoreCollision(_overlaps[i], _col, !_isGround);
                }
                else if (hit.layer == LayerMask.NameToLayer("Ladder"))
                {
                    _isClimbing = true;
                }
            }
        }

        //public void AnimateMario()
        //{
        //    if (_direction.x < 0f)
        //        _spriteRenderer.flipX = true;

        //    else
        //        _spriteRenderer.flipX = false;

        //    if (_direction.x != 0f)
        //        _animator.SetBool("isRunning", true);

        //    else
        //        _animator.SetBool("isRunning", false);

        //    if (_direction.y != 0f)
        //        _animator.SetBool("isClimbing", true);

        //    else
        //    {
        //        _animator.SetBool("isClimbing", false);
        //        _animator.SetTrigger("Climb");
        //    }

        //}

    }
}