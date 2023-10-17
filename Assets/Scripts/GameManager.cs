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

        private int _score;
        private int _lives;
        private int _currentLevel;

        //static instance variable to make it a Singleton
        public static GameManager _instance;

        #endregion

        #region Methods

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            NewGame();
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
            Invoke(nameof(LoadingScene), 2f);
        }

        private void NewGame()
        {
            _lives = 3;
            _score = 0;
            LoadLevel(2);
        }

        public void OnLevelCompleted()
        {
            _score = 1000;
            int levelIndex = _currentLevel + 1;

            //User Configuration File
            levelIndex = PlayerPrefs.GetInt("CurrentLevel", levelIndex);

            if (levelIndex < SceneManager.sceneCountInBuildSettings)
                LoadLevel(levelIndex);
            else
                LoadLevel(2);

        }
        
        public void OnLevelFailed()
        {
            _lives--;
            if (_lives <= 0)
                NewGame();
            else
            {
                LoadLevel(_currentLevel);
            }
        }

        #endregion

    }
}