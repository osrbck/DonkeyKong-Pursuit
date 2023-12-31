using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class KongAgent : MonoBehaviour
    {
        private void OnEnable() { InvokeRepeating(nameof(AnimateKong), 1f / 10f, 1f / 3f); }
        private void OnDisable() { CancelInvoke(); }

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] _kongSprites;
        private int spriteID;

        public void AnimateKong()
        {
            if (spriteID >= _kongSprites.Length)
                spriteID = 0;
            spriteRenderer.sprite = _kongSprites[spriteID];
            spriteID++;
        }
    }
}