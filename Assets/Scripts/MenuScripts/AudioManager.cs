using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace DonkeyKongPursuit
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance{ get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance == this)
                Destroy(this.gameObject);
            else
                Instance = this;

            if (PlayerPrefs.GetInt("MusicMuted", 0) == 0)
                IsMusicMuted = false;
            else
                IsMusicMuted = true;
            if (PlayerPrefs.GetInt("SoundMuted", 0) == 0)
                IsSoundMuted = false;
            else
                IsSoundMuted = true;
        }


        [SerializeField] private AudioSource _musicPlayer;
        [SerializeField] private AudioSource[] _soundChannels;
        [SerializeField] private bool _isMusicMuted;
        [SerializeField] private bool _isSoundMuted;

        public bool IsMusicMuted
        {
            get { return _isMusicMuted; }
            set
            {
                _isMusicMuted = value;
                _musicPlayer.mute = _isMusicMuted;
                PlayerPrefs.SetInt("MusicMuted", _isMusicMuted ? 1 : 0);
            }
        }

        public bool IsSoundMuted
        {
            get { return _isSoundMuted; }
            set
            {
                _isSoundMuted = value;
                foreach (var channel in _soundChannels)
                {
                    channel.mute = _isSoundMuted;
                }
                _isSoundMuted = value;
                PlayerPrefs.SetInt("SoundMuted", _isSoundMuted ? 1 : 0);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicPlayer.clip = clip;
            _musicPlayer.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            foreach (var channel in _soundChannels)
            {
                if (!channel.isPlaying)
                {
                    channel.PlayOneShot(clip);
                    break;
                }
            }
        }

    }
}