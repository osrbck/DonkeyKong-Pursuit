using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonkeyKongPursuit
{
    /// <summary>
    /// Play, Quit Buttons
    /// Load the "Preload" scene Or Quit Aplication
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
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