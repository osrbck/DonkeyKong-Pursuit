using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class MarioController : MonoBehaviour
    {
        private MarioData _marioData;
        public MarioData MarioData { get { return _marioData; } }

        //[SerializeField] private AudioClip _sfxFailed;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxJump;
        [SerializeField] private AudioSource _sfxWin;
        [SerializeField] private AudioSource _sfxFailed;

        private Rigidbody2D _rigidbody;
        private Collider2D _col;
        private Collider2D[] _colOverlaps;

        [SerializeField] private bool _isGround;
        public bool IsGround { get { return _isGround; } }
        [SerializeField] private bool _isClimbing;
        public bool IsClimbing { get { return _isClimbing; } }

        private Vector2 _direction;
        public Vector2 Direction { get { return _direction; } }

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
            _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.collider.tag == "Danger" || collision.collider.tag == "Barrel")
            {
                Destroy(gameObject);
                _sfxFailed.Play();
                _musicSource.Pause();
                GameManager.Instance.OnLevelFailed();

            }
            else if (collision.collider.tag == "Princess")
            {
                enabled = false;
                _sfxWin.Play();
                _musicSource.Pause();
                GameManager.Instance.OnLevelComplated();
            }
        }


        private void SetDirection()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if (_isClimbing)
            {
                _direction.y = moveY * _marioData.ClimbSpeed * Time.fixedDeltaTime;
                _rigidbody.gravityScale = 0.3f;

            }

            else if (_isGround && Input.GetKey(KeyCode.W))
            {
                _direction = Vector2.up * _marioData.JumpSpeed * Time.fixedDeltaTime;
                _sfxJump.Play();
            }

            else
            {
                _direction += Physics2D.gravity * Time.fixedDeltaTime;
            }

            _direction.x = moveX * _marioData.MoveSpeed * Time.fixedDeltaTime;

            // Prevent gravity from building up infinitely
            if (_isGround)
            {
                _direction.y = Mathf.Max(_direction.y, -1f);
            }

            if (_direction.x > 0f)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (_direction.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }

        private void MoveCollisionCheck()
        {
            _isGround = false;
            _isClimbing= false;

            // the amount that two colliders can overlap
            // increase this value for steeper platforms
            float skinWidth = 0.1f;
            Vector2 size = _col.bounds.size;
            size.y += skinWidth;
            size.x /= 1.5f;
            int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, _colOverlaps);

            for (int i = 0; i < amount; i++)
            {
                GameObject hit = _colOverlaps[i].gameObject;

                if (hit.layer == LayerMask.NameToLayer("Platform"))
                {

                    // Only set as grounded if the platform is below the player
                    _isGround = hit.transform.position.y < (transform.position.y - 0.5f + skinWidth);

                    // Turn off collision on platforms the player is not grounded to
                    Physics2D.IgnoreCollision(_colOverlaps[i], _col, !_isGround);
                }
                else if (hit.layer == LayerMask.NameToLayer("Ladder"))
                {
                    _isClimbing = true;
                }


            }
        }

    }
}