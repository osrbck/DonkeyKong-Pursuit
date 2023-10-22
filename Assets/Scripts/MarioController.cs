using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class MarioController : MonoBehaviour
    {
        #region Fields
        private MarioData _marioData;
        public MarioData MarioData { get { return _marioData; } }
        [SerializeField] private PauseMenu _pause;
        [SerializeField] private AudioSource _sfxJump;
        [SerializeField] private AudioSource _sfxWin;
        [SerializeField] private AudioSource _sfxFailed;
        [SerializeField] private AudioSource _musicSource;

        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Collider2D _col;
        private Collider2D[] _colOverlaps;
        [SerializeField] private bool _isClimbing;
        [SerializeField] private bool _isGround;
        [SerializeField] private bool _jumpReady;
        [SerializeField] private float _jumpCD = 0.8f;
        #endregion

        #region Properties
        public Vector2 Direction { get { return _direction; } }
        public bool IsGround { get { return _isGround; } }
        public bool IsClimbing { get { return _isClimbing; } }
        #endregion

        #region Methods
        void Start()
        {
            _marioData = GetComponent<MarioData>();
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
            if (_col == null)
                _col = GetComponent<Collider2D>();
            if (_colOverlaps == null)
                _colOverlaps = new Collider2D[4];
            _musicSource.Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale != 0)
                    MenuManager.GoToMenu(MenuName.Pause);
            }
        }

        private void FixedUpdate()
        {
            MoveCollisionCheck();
            SetDirection();
            // Move Mario accordingly
            _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Mario loses and the game responds accordingly
            if (collision.collider.tag == "Danger" || collision.collider.tag == "Barrel")
            {
                Destroy(gameObject);
                _sfxFailed.Play();
                _musicSource.Pause();
                GameManager.Instance.OnLevelFailed();
            }
            // Mario wins and the game responds accordingly
            else if (collision.collider.tag == "Princess")
            {
                enabled = false;
                _sfxWin.Play();
                _musicSource.Pause();
                GameManager.Instance.OnLevelComplated();
            }
        }

        /// <summary>
        /// Sets the movement direction for the character based on input and game state.
        /// Handles climbing, jumping, and gravity.
        /// </summary>
        private void SetDirection()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if (_isGround && Input.GetKey(KeyCode.Space) &&_jumpReady)
            {
                _direction = Vector2.up * _marioData.JumpSpeed * Time.fixedDeltaTime;
                _sfxJump.Play();
                _jumpCD = 0.8f;
            }
            else if(_isClimbing)
            {
                _direction.y = moveY * _marioData.ClimbSpeed * Time.fixedDeltaTime;
                _rigidbody.gravityScale = 0.4f;

                if (_isClimbing && !_isGround)
                    _direction.x = 0f;
                else if (_isGround)
                    _direction.x = moveX * _marioData.MoveSpeed * Time.fixedDeltaTime;
            }
            // Set horizontal movement and apply gravity when not climbing or jumping
            else
            {
                _direction.x = moveX * _marioData.MoveSpeed * Time.fixedDeltaTime;
                _direction += Physics2D.gravity * Time.fixedDeltaTime;
            }
            // Prevent gravity from building up infinitely
            if (_isGround)
            {
                _direction.y = Mathf.Max(_direction.y, -1f);
            }
            // Jump cooldown
            if (_jumpCD < 0f)
                _jumpReady = true;
            else
            {
                _jumpCD = _jumpCD - Time.deltaTime;
                _jumpReady = false;
            }
        }

        /// <summary>
        /// Checks for collisions to determine Mario grounded or climbing.
        /// </summary>
        private void MoveCollisionCheck()
        {
            _isGround = false;
            _isClimbing= false;

            // the amount that two colliders can overlap 
            // increase this value for steeper platforms
            float skinWidth = 0.1f;
            Vector2 size = _col.bounds.size;
            size.y += skinWidth;
            size.x /= 0.5f;
            int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, _colOverlaps);

            for (int i = 0; i < amount; i++)
            {
                GameObject hit = _colOverlaps[i].gameObject;
                if (hit.layer == LayerMask.NameToLayer("Platform"))
                {
                    _isGround = hit.transform.position.y < (transform.position.y - 0.5f + skinWidth);
                    // Turn off collision on platforms the player is not grounded to
                    Physics2D.IgnoreCollision(_colOverlaps[i], _col, !_isGround);
                }
                else if (hit.layer == LayerMask.NameToLayer("Ladder") && _jumpReady)
                {
                    _isClimbing = true;
                }

            }
        }
        #endregion

    }
}