using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class EnemyFlareController : MonoBehaviour
    {
        [SerializeField] private float _flareMovement;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private int _patrolDestination;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _flareSprites;
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
            if (_spriteRenderer == null)
                _spriteRenderer= GetComponent<SpriteRenderer>();
            if (_flareSprites == null)
                _flareSprites = GetComponent<Sprite[]>();
            if(_patrolPoints == null)
                _patrolPoints = GetComponent<Transform[]>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[0].position, _flareMovement * Time.deltaTime);
                if(Vector2.Distance(transform.position, _patrolPoints[0].position)< 1f)
                    _patrolDestination = 1;
            }

            if (_patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[1].position, _flareMovement * Time.deltaTime);
                if (Vector2.Distance(transform.position, _patrolPoints[1].position) < 1f)
                    _patrolDestination = 0;
            }

        }
        public void AnimateFlare()
        {
            if (_patrolDestination == 1)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }

            if (transform.position.x != 0f)
            {
                spriteID++;
                if (spriteID >= _flareSprites.Length)
                    spriteID = 0;
                _spriteRenderer.sprite = _flareSprites[spriteID];
            }

                
        }
    }
}