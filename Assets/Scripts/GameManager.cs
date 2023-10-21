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
        [SerializeField] AudioSource _levelMusicSource;
        [SerializeField] private int _score;
        [SerializeField] private int _lives;
        [SerializeField] private int _currentLevel;
        #endregion

        #region Properties
        //static instance variable to make it a Singleton
        public static GameManager Instance { get; private set; }
        public int PlayerLives { get { return _lives; } }
        #endregion

        #region Methods
        /// <summary>
        /// Singleton initialization
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
        /// <summary>
        /// Initialization of the GameManager with a new game
        /// </summary>
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            NewGame();
        }

        public void DestroyManager()
        {
            Destroy(gameObject);
        }
        /// <summary>
        /// Loads a game level by index with time delay
        /// </summary>
        /// <param name="levelIndex"></param>
        private void LoadLevel(int levelIndex)
        {
            _currentLevel = levelIndex;

            Camera cam = Camera.main;
            if (cam != null)
                cam.cullingMask = 0;

            Invoke(nameof(LoadingScene), 3.5f);
        }
        private void LoadingScene()
        {
            SceneManager.LoadScene(_currentLevel);
        }

        /// Loading a new game - Sceene 2 - Kong Level 1
        private void NewGame()
        {
            _lives = 3;
            _score = 0;
            LoadLevel(2);
        }

        public void OnLevelComplated()
        {
            _score += 1000;
            int levelIndex = _currentLevel + 1;

            //User Configuration
            levelIndex = PlayerPrefs.GetInt("CurrentLevel", levelIndex);

            if (levelIndex < SceneManager.sceneCountInBuildSettings)
                LoadLevel(levelIndex);
            else
                MenuManager.GoToMenu(MenuName.Main);
        }

        public void OnLevelFailed()
        {
            _lives--;
            if (_lives <= 0)
                MenuManager.GoToMenu(MenuName.GameOver);
            else
                LoadLevel(_currentLevel);
        }

        #endregion

    }
}