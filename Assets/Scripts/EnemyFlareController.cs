using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class EnemyFlareController : MonoBehaviour
    {
        [SerializeField] private Transform[] _partrolPoints;
        [SerializeField] private float _flareMovement;
        [SerializeField] private int _patrolDestination;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] runSprites;
        private int spriteID;

        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateFlare), 1f / 15f, 1f / 14f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            if (runSprites == null)
                runSprites = GetComponent<Sprite[]>();
            if(_partrolPoints == null)
            _partrolPoints = GetComponent<Transform[]>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _partrolPoints[0].position, _flareMovement * Time.deltaTime);
                if(Vector2.Distance(transform.position, _partrolPoints[0].position)< 1f)
                    _patrolDestination = 1;
            }

            if (_patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, _partrolPoints[1].position, _flareMovement * Time.deltaTime);
                if (Vector2.Distance(transform.position, _partrolPoints[1].position) < 1f)
                    _patrolDestination = 0;
            }

        }
        public void AnimateFlare()
        {
            if (_patrolDestination == 1)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            if (transform.position.x != 0f)
            {
                spriteID++;
                if (spriteID >= runSprites.Length)
                    spriteID = 0;
                spriteRenderer.sprite = runSprites[spriteID];
            }

                
        }
    }
}