using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class PatrolController : MonoBehaviour
    {
        // Animate Patrol character at a regular interval.
        private void OnEnable() { InvokeRepeating(nameof(AnimateFlare), 1f / 3f, 1f / 5f); }
        private void OnDisable() { CancelInvoke(); }

        #region Fields
        [SerializeField] private float _flareMovement;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private int _patrolDestination;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _flareSprites;
        private int spriteID;
        #endregion

        #region Methods
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
            // Move towards the first patrol point.
            if (_patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[0].position, _flareMovement * Time.deltaTime);
                if(Vector2.Distance(transform.position, _patrolPoints[0].position)< 1f)
                    _patrolDestination = 1;
            }
            // Move towards the second patrol point.
            if (_patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[1].position, _flareMovement * Time.deltaTime);
                if (Vector2.Distance(transform.position, _patrolPoints[1].position) < 1f)
                    _patrolDestination = 0;
            }

        }
        /// <summary>
        /// Animate the Flare character based on its movement and orientation.
        /// </summary>
        public void AnimateFlare()
        {
            if (_patrolDestination == 1)
                _spriteRenderer.flipX = true;
            else
                _spriteRenderer.flipX = false;

            if (transform.position.x != 0f)
            {
                spriteID++;
                if (spriteID >= _flareSprites.Length)
                    spriteID = 0;
                _spriteRenderer.sprite = _flareSprites[spriteID];
            }  
        }
        #endregion
    }
}