using System.Collections;
using System.Collections.Generic;
using DonkeyKongPursuit;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DonkeyKongPursuit
{

    public class UISettings : GameBehaviur
    {
        public Toggle MuteMusicToggle;
        public Toggle MuteSfxToggle;

        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);

            if (gameManager.audioPlayer.IsMusicMuted)
                MuteMusicToggle.isOn = true;
            else
                MuteMusicToggle.isOn = false;

            if (gameManager.audioPlayer.IsSoundMuted)
                MuteSfxToggle.isOn = true;
            else
                MuteSfxToggle.isOn = false;

            MuteMusicToggle.onValueChanged.AddListener(delegate { MuteMusic(); });
            MuteSfxToggle.onValueChanged.AddListener(delegate { MuteSfx(); });

        }


        public void MuteMusic()
        {
            _gameManager.audioPlayer.IsMusicMuted = MuteMusicToggle.isOn;
        }
        public void MuteSfx()
        {
            _gameManager.audioPlayer.IsSoundMuted = MuteSfxToggle.isOn;
        }
    }
}
