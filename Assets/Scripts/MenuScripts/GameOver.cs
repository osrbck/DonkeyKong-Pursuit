using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonkeyKongPursuit
{
    public class GameOver : MonoBehaviour
    {
        public void HandleRestartButtonOnClick()
        {
            GameManager.Instance.DestroyManager();
            SceneManager.LoadScene("Preload");
            Destroy(gameObject);
        }

        public void HandleQuitButtonOnClick()
        {
            Destroy(gameObject);
            MenuManager.GoToMenu(MenuName.Main);
        }
    }
}
