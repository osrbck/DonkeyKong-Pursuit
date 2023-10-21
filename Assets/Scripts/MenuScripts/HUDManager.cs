using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    /// <summary>
    // Update the lives display text to the match player lives accurately.
    /// </summary>
    public class HUDManager : MonoBehaviour
    {
        public int livesCounter;

        public Text _livesText;

        private void Awake()
        {
            _livesText = GetComponentInChildren<Text>();
        }
        private void Start()
        {
            livesCounter = GameManager.Instance.PlayerLives;
        }

        private void Update()
        {
            _livesText.text = "x " + (livesCounter-1);
        }

    }
}