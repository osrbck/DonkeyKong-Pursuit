using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{
    public class PauseMenu : MonoBehaviour
    {


        void Start()
        {
            Time.timeScale = 0;
        }

        public void HandleResumeButtonOnClick()
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

        public void HandleQuitButtonOnClick()
        {
            Time.timeScale = 1;
            Destroy(gameObject);
            MenuManager.GoToMenu(MenuName.Main);
        }


        

    }
}