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
                    GameManager.Instance.DestroyManager();
                    break;

                case MenuName.Pause:
                    //Instantiate Prefab
                    Object.Instantiate(Resources.Load("PauseMenu"));
                    break;

                case MenuName.GameOver:
                    //Instantiate Prefab
                    Object.Instantiate(Resources.Load("GameOver"));
                    break;
            }
        }
    }
}