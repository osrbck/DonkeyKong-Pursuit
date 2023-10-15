using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jumpSpeed;

    [SerializeField] private float _climbSpeed;


    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    public float JumpSpeed
    {
        get { return _jumpSpeed; }
        set { _jumpSpeed = value; }
    }

    public float ClimbSpeed
    {
        get { return _climbSpeed; }
        set { _climbSpeed = value; }
    }
}
