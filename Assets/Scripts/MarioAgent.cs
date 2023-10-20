using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class MarioAgent : MonoBehaviour
    {
        [SerializeField] private MarioController _marioCtrl;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _climbSprite;
        [SerializeField] private Sprite[] _runSprites;
        private int spriteID;

        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateMario), 1f / 10f, 1f / 10f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
        // Start is called before the first frame update
        void Start()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_climbSprite == null)
                _climbSprite = GetComponent<Sprite>();
            if (_runSprites == null)
                _runSprites = GetComponent<Sprite[]>();
        }

        public void AnimateMario()
        {

            if (_marioCtrl.Direction.x != 0f && _marioCtrl.IsGround)
            {
                spriteID++;
                if (spriteID >= _runSprites.Length)
                    spriteID = 0;
                _spriteRenderer.sprite = _runSprites[spriteID];
            }

            else if (_marioCtrl.IsClimbing && !_marioCtrl.IsGround)
            {
                _spriteRenderer.sprite = _climbSprite;
            }
            else
                _spriteRenderer.sprite = _runSprites[spriteID];

        }
    }
}