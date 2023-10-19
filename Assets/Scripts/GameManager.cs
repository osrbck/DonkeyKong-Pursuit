using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonkeyKongPursuit
{
    public class GameManager : MonoBehaviour
    {

        #region Fields

        [SerializeField] private int _score;
        [SerializeField] private int _lives;
        [SerializeField] private int _currentLevel;

        public int PlayerLives { get { return _lives; } }
        //static instance variable to make it a Singleton
        public static GameManager Instance { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            NewGame();
        }

        public void DestroyManager()
        {
            Destroy(gameObject);
        }

        private void NewGame()
        {
            _lives = 3;
            _score = 0;
            LoadLevel(2);
        }

        private void LoadingScene()
        {
            SceneManager.LoadScene(_currentLevel);
        }

        private void LoadLevel(int levelIndex)
        {
            _currentLevel = levelIndex;

            Camera cam = Camera.main;
            if (cam != null)
                cam.cullingMask = 0;

            Invoke(nameof(LoadingScene), 2.3f);
        }


        public void OnLevelComplated()
        {
            _score += 1000;
            int levelIndex = _currentLevel + 1;

            //User Configuration File
            levelIndex = PlayerPrefs.GetInt("CurrentLevel", levelIndex);

            if (levelIndex < SceneManager.sceneCountInBuildSettings)
                LoadLevel(levelIndex);
            else
            {
                LoadLevel(0);
            }
        }

        public void OnLevelFailed()
        {
            _lives--;
            if (_lives <= 0)
            {
                MenuManager.GoToMenu(MenuName.GameOver);
            }

            else
            {
                LoadLevel(_currentLevel);
            }
        }

        #endregion

    }
}