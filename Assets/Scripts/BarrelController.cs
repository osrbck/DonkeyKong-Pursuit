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
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //RIGHT represents red-X-arrow of platforms
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _rigidbody.AddForce(collision.transform.right * _barrelSpeed, ForceMode2D.Impulse);
        }
    }
}