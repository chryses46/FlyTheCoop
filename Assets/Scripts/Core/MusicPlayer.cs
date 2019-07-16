using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyTheCoop.Core
{
    public class MusicPlayer : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] AudioClip introMusic;
        [SerializeField] AudioClip gameMuisc;
        [SerializeField] AudioClip winMusic;
#endregion
#region TypeReferences
        AudioSource audioSource;
#endregion
#region Startup
        void OnEnable()
        {
            ThereCanOnlyBeOne();
            audioSource = GetComponent<AudioSource>();
        }
#endregion
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
            Debug.Log("PlayWinMusic called");
            if(audioSource)
            {
                audioSource.clip = winMusic;
                audioSource.Play();
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
