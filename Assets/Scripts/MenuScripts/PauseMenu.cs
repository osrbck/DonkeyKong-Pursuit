using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool _gameIsPaused = false;
        void Start()
        {
            Time.timeScale = 0;
            _gameIsPaused = true;
        }

        public void HandleResumeButtonOnClick()
        {
            Time.timeScale = 1;
            Destroy(gameObject);
            _gameIsPaused = false;
        }

        public void HandleQuitButtonOnClick()
        {
            Time.timeScale = 1;
            Destroy(gameObject);
            MenuManager.GoToMenu(MenuName.Main);
        }


        

    }
}