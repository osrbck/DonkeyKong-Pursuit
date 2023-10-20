using System.Collections;
using System.Collections.Generic;
using DonkeyKongPursuit;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
namespace DonkeyKongPursuit
{
    public class UISettings : MonoBehaviour
    {
        public Toggle MuteMusicToggle;
        public Toggle MuteSfxToggle;

        public Toggle FullScreen;

        private void Start()
        {

            if (AudioManager.Instance.IsMusicMuted)
                MuteMusicToggle.isOn = true;
            else
                MuteMusicToggle.isOn = false;

            if (AudioManager.Instance.IsSoundMuted)
                MuteSfxToggle.isOn = true;
            else
                MuteSfxToggle.isOn = false;

            MuteMusicToggle.onValueChanged.AddListener(delegate { MuteMusic(); });
            MuteMusicToggle.onValueChanged.AddListener(delegate { MuteSfx(); });

        }
        public void MuteMusic()
        {
            AudioManager.Instance.IsMusicMuted = MuteMusicToggle.isOn;
        }
        public void MuteSfx()
        {
            AudioManager.Instance.IsSoundMuted = MuteSfxToggle.isOn;
        }

        public void IsFullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
            Debug.Log("ScreenRes");
        }
    }
}