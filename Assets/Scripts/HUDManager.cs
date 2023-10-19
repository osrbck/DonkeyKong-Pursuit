using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class HUDManager : MonoBehaviour
    {
        public int _livesCounter;

        public Text _livesText;

        private void Awake()
        {
            _livesText = GetComponentInChildren<Text>();
        }
        private void Start()
        {
            _livesCounter = GameManager.Instance.PlayerLives;
        }

        private void Update()
        {
            _livesText.text = "x" + (_livesCounter-1);
        }

    }
}