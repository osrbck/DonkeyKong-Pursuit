using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class MarioController : MonoBehaviour
    {
        private PauseMenu _pauseMenu;

        private MarioData _marioData;
        public MarioData MarioData { get { return _marioData; } }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _climbSprite;
        [SerializeField] private Sprite[] _runSprites;
        private int spriteID;

        [SerializeField] private bool _isGround;
        [SerializeField] private bool _isClimbing;

        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Collider2D _col;
        private Collider2D[] _colOverlaps;

        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateMario), 1f / 15f, 1f / 14f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        void Start()
        {
            _marioData = GetComponent<MarioData>();
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_climbSprite == null)
                _climbSprite = GetComponent<Sprite>();
            if (_runSprites == null)
                _runSprites = GetComponent<Sprite[]>();
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
            if (_col == null)
                _col = GetComponent<Collider2D>();
            _colOverlaps = new Collider2D[4];
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.GoToMenu(MenuName.Pause);
            }
        }

        private void FixedUpdate()
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
                GameManager.Instance.OnLevelFailed();
            }
            else if (collision.collider.tag == "Princess")
            {
                enabled = false;
                GameManager.Instance.OnLevelComplated();
            }
        }


        private void SetDirection()
        {
            if (_isClimbing)
            {
                _direction.y = Input.GetAxis("Vertical") * _marioData.ClimbSpeed * Time.fixedDeltaTime;
                _rigidbody.gravityScale = 0f;
            }

            else if (_isGround && (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.W)))
            {
                _direction = Vector2.up * _marioData.JumpSpeed * Time.fixedDeltaTime;
            }

            else
            {
                _direction += Physics2D.gravity * Time.fixedDeltaTime;
            }

            _direction.x = Input.GetAxis("Horizontal") * _marioData.MoveSpeed * Time.fixedDeltaTime;


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

        private void CheckCollision()
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

        public void AnimateMario()
        {
            if (_isClimbing && !_isGround)
            {
                _spriteRenderer.sprite = _climbSprite;
            }

            else if (_direction.x != 0f && _isGround)
            {
                spriteID++;
                if (spriteID >= _runSprites.Length)
                    spriteID = 0;
                _spriteRenderer.sprite = _runSprites[spriteID];
            }

        }

    }
}