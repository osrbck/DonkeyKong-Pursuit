using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class MarioAgent : MonoBehaviour
    {
        // Animate Mario character at a regular interval.
        private void OnEnable() { InvokeRepeating(nameof(AnimateMario), 1f / 6f, 1f / 6f); }
        private void OnDisable() { CancelInvoke(); }

        #region Fields
        [SerializeField] private MarioController _marioCtrl;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _climbSprites;
        [SerializeField] private Sprite[] _runSprites;
        private int climbID;
        private int runID;
        #endregion

        #region Methods
        void Start()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_climbSprites == null)
                _climbSprites = GetComponent<Sprite[]>();
            if (_runSprites == null)
                _runSprites = GetComponent<Sprite[]>();
        }
        /// <summary>
        /// Animate the Mario character based on its state and direction.
        /// Set the sprite to the current frame of the climb animation
        /// Ensure the animation IDs does not exceed the number of the sprites
        /// </summary>
        public void AnimateMario()
        {
            // Check if Mario is running on the ground
            if (_marioCtrl.Direction.x != 0f && _marioCtrl.IsGround)
            {
                runID++;
                if (runID>= _runSprites.Length)
                    runID = 0;
                _spriteRenderer.sprite = _runSprites[runID];
            }
            // Check if Mario is climbing and not on the ground
            else if (_marioCtrl.IsClimbing && !_marioCtrl.IsGround)
            {
                if (_marioCtrl.Direction.y != 0)
                {
                    climbID++;
                    if (climbID >= _climbSprites.Length)
                        climbID = 0;
                    _spriteRenderer.sprite = _climbSprites[climbID];
                }
                else
                    _spriteRenderer.sprite = _climbSprites[climbID];
            }

            else
                _spriteRenderer.sprite = _runSprites[runID];

            // Adjust character orientation
            if (_marioCtrl.Direction.x > 0f)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (_marioCtrl.Direction.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
        #endregion
    }
}