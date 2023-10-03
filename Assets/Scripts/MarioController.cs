using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    [SerializeField] private float _speed;


    void Start()
    {
        if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
     _direction.x = Input.GetAxis("Horizontal") * _speed;
     _direction.y = Input.GetAxis("Vertical") * _speed;
    }


    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);
    }

}
