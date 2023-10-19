using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonkeyKongPursuit
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameManager GM;

        public void HandlePlayButtonOnClick()
        {
            SceneManager.LoadScene("Preload");
            Destroy(gameObject);
        }

        public void HandleQuitButtonOnClick()
        {
            Application.Quit();
        }
    }
}