using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Collider2D _col;
    private Vector2 _direction;

    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private bool _isGround;
    [SerializeField] private Transform _gizmo;


    void Start()
    {
        if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_col == null)
            _col= GetComponent<Collider2D>();

        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_gizmo == null)
            _gizmo = transform.Find("Gizmo");
    }

    private void CheckCollision()
    {

    }
    void FixedUpdate()
    {
        CheckCollision();

        int layerMask = LayerMask.GetMask("Ground");

        _isGround = Physics2D.OverlapPoint(_gizmo.position, layerMask);
        float moveX = Input.GetAxis("Horizontal");

        if (moveX < 0f)
            _spriteRenderer.flipX = true;

        else
            _spriteRenderer.flipX = false;

        if ( _isGround && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            _rigidbody.AddForce(Vector2.up * _jump * Time.fixedDeltaTime, ForceMode2D.Impulse);


            if (_direction.x > 0f)
                transform.eulerAngles = Vector3.zero;
            else if (_direction.x < 0f)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        Vector2 newVelocity = new Vector2(moveX * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);

        _rigidbody.velocity = newVelocity;
    }

}