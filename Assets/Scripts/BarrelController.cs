using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class BarrelController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        [SerializeField] private float _barrelSpeed = 1f;

        private void Awake()
        {
            if(_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            //_rigidbody.AddForce(Vector2.right * _barrelSpeed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //RIGHT represents red-X-arrow of platforms
            if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
                _rigidbody.AddForce(collision.transform.right * _barrelSpeed, ForceMode2D.Impulse);
        }
    }
}