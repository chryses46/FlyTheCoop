using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip introMusic;
        [SerializeField] AudioClip gameMuisc;
        [SerializeField] AudioClip winMusic;
        AudioSource audioSource;

        void OnEnable()
        {
            ThereCanOnlyBeOne();
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayGameMusic()
        {
            if(audioSource)
            {
                audioSource.clip = gameMuisc;
                audioSource.Play();
            }
        }

        public void PlayWinMusic()
        {
            if(audioSource)
            {
                audioSource.clip = winMusic;
                audioSource.Play();
            }
        }

        public void PlayIntroMusic()
        {
            if(audioSource)
            {
                if(audioSource.clip != introMusic)
                {
                    audioSource.clip = introMusic;
                    audioSource.Play();
                }else if(audioSource.clip == introMusic)
                {
                    return;
                }
            }
        }

        private void ThereCanOnlyBeOne()
        {
            int these = FindObjectsOfType<MusicPlayer>().Length;
            if (these > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
