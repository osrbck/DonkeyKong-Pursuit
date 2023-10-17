using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class MarioController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Collider2D _col;
        private Collider2D[] _overlaps = new Collider2D[4];
        private Vector2 _direction;
        private MarioData _data;


        [SerializeField] private bool _isClimbing;
        [SerializeField] private bool _isGround;

        private SpriteRenderer _spriteRenderer;
        public Sprite[] _runSprites;
        public Sprite _climbSprite;
        private int _spriteIndex;

        void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
            if (_col == null)
                _col = GetComponent<Collider2D>();
            if (_overlaps == null)
                _overlaps = GetComponent<Collider2D[]>();
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            _data = GetComponent<MarioData>();
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateMario), 1f / 12f, 1f / 12f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Update()
        {
            //SetDirection();
            //CheckCollision();
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
                FindObjectOfType<GameManager>().OnLevelFailed();
            }
            else if(collision.collider.tag == "Danger")
            {
                enabled = false;
                FindObjectOfType<GameManager>().OnLevelCompleted();
            }
        }

        private void SetDirection()
        {
            if (_isClimbing)
            {
                _direction.y = Input.GetAxis("Vertical") * _data.ClimbSpeed * Time.deltaTime;
                _rigidbody.gravityScale = 0f;
            }
            else if (_isGround && (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.W)))
            {
                _direction = Vector2.up * _data.JumpSpeed * Time.deltaTime;
            }
            else
            {
                _direction += Physics2D.gravity * Time.deltaTime;
            }

            _direction.x = Input.GetAxis("Horizontal") * _data.MoveSpeed * Time.deltaTime;

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

        public void AnimateMario()
        {
            if (_isClimbing) _spriteRenderer.sprite = _climbSprite;
            else if (_direction.x != 0f)
            {
                _spriteIndex++;
                if (_spriteIndex >= _runSprites.Length)
                {
                    // Reset the index if it exceeds the array length
                    _spriteIndex = 0;
                }
                _spriteRenderer.sprite = _runSprites[_spriteIndex];
            }
        }

    }
}