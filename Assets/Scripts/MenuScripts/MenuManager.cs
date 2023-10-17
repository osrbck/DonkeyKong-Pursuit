using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonkeyKongPursuit
{
    public static class MenuManager
    {
        public static void GoToMenu(MenuName name)
        {
            switch (name)
            {
                case MenuName.Main:
                    //Load Menu
                    SceneManager.LoadScene("MainMenu");
                    break;

                case MenuName.Pause:
                    //Instantiate Prefab
                    Object.Instantiate(Resources.Load("PauseMenu"));
                    break;
            }
        }
    }
}