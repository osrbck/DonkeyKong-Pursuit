using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _col;
    private Collider2D[] _overlaps = new Collider2D[4];
    private Vector2 _direction;

    private PlayerData _data;

    [SerializeField] private bool _isClimbing;
    [SerializeField] private bool _isGround;
    [SerializeField] private Transform _gizmo;


    void Start()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
        if (_col == null)
            _col = GetComponent<Collider2D>();
        if (_overlaps == null)
            _overlaps = GetComponent<Collider2D[]>();
        if (_direction == null)
            _direction = GetComponent<Vector2>();
        if (_gizmo == null)
            _gizmo = transform.Find("Gizmo");
    }

    private void Update()
    {
        SetDirection();
        CheckCollision();
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);

    }

    private void SetDirection()
    {
        if (_isClimbing)
        {
            _direction.y = Input.GetAxis("Vertical") * _data.ClimbSpeed * Time.deltaTime;
        }
        else if (_isGround && Input.GetButtonDown("Jump"))
        {
            _direction = Vector2.up * _data.JumpSpeed;
        }
        else
        {
            _direction += Physics2D.gravity * Time.deltaTime;
        }

        _direction.x = Input.GetAxis("Horizontal") * _data.MoveSpeed;

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

}
