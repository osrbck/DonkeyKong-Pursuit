using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class MarioController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private Collider2D _col;
        private Collider2D[] _overlaps = new Collider2D[4];
        private Vector2 _direction;

        [SerializeField] private float _speed;
        [SerializeField] private float _jump;
        [SerializeField] private float _climb;
        [SerializeField] private bool _isClimbing;
        [SerializeField] private bool _isGround;
        [SerializeField] private Transform _gizmo;


        void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            if (_col == null)
                _col = GetComponent<Collider2D>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_gizmo == null)
                _gizmo = transform.Find("Gizmo");
        }

        private void Update()
        {
        CheckCollision();
            SetDirection();
        }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);

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
    private void SetDirection()
    {
        if (_isClimbing)
        {
            _direction.y = Input.GetAxis("Vertical") * _speed;
        }
        else if (_isGround && Input.GetButtonDown("Jump"))
        {
            _direction = Vector2.up * _jump;
        }
        else
        {
            _direction += Physics2D.gravity * Time.deltaTime;
        }

        _direction.x = Input.GetAxis("Horizontal") * _speed;

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


}
