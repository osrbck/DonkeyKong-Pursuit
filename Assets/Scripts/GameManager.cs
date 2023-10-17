using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonkeyKongPursuit
{
    public class GameManager : MonoBehaviour
    {

        private int _score;
        private int _lives;
        private int _currentLevel;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            NewGame();
        }

        private void NewGame()
        {
            _lives = 3;
            _score = 0;
            LoadLevel(1);
            // Load Game
        }

        private void LoadLevel(int levelIndex)
        {
            _currentLevel = levelIndex;

            Camera cam = Camera.main;
            if (cam != null)
                cam.cullingMask = 0;
            Invoke(nameof(LoadingScene), 2f);
        }

        private void LoadingScene()
        {
            SceneManager.LoadScene(_currentLevel);
        }

        public void OnLevelCompleted()
        {
            _score = 1000;
            int levelIndex = _currentLevel++;

            //User Configuration File
            levelIndex = PlayerPrefs.GetInt("CurrentLevel", levelIndex);

            if (levelIndex < SceneManager.sceneCountInBuildSettings)
                LoadLevel(levelIndex);
            else
                LoadLevel(1);

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

    }
}