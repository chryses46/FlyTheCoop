using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class HUDController : MonoBehaviour
    {
        
        [SerializeField] Text eggCountText;

        [SerializeField] AudioClip collectEgg;

        [SerializeField] int requiredModifier = 3;

        PlayerPrefConfig ppc;
        AudioSource audioSource;

        private int TotalEggsRequired { get; set; } // Increasing value for NormalMode play

        const int levelOneRequiredEggs = 2; // const to base TotalEgg formula around

        private int LevelScore { get; set; }
        public int TotalScore { get; private set; }

        void OnEnable()
        {
            ppc = FindObjectOfType<PlayerPrefConfig>();
            audioSource = GetComponent<AudioSource>();

        }

        public void EggCollected()
        {
            Debug.Log("Egg Collected.");
            audioSource.PlayOneShot(collectEgg);
            LevelScore += 1;
            eggCountText.text = (LevelScore + TotalScore).ToString();
        }

        public void UpdateTotalScore()
        {
            TotalScore += LevelScore;
            ResetLevelScore();
        }

        public void UpdateHighScore()
        {
            if(TotalScore > ppc.HighScore)
            {
                ppc.ManageHighScore(TotalScore);
            }
        }

        public void ResetScore()
        {
            TotalScore = 0;
            eggCountText.text = TotalScore.ToString();
        }

        public void ResetLevelScore()
        {
            LevelScore = 0;
            eggCountText.text = (LevelScore + TotalScore).ToString();
        }

        public void UpdateRequiredEggsCount(int currentLevelIndex)
        {
            TotalEggsRequired = levelOneRequiredEggs+currentLevelIndex * requiredModifier;

            Debug.Log("Total Eggs Required: " + TotalEggsRequired);
            Debug.Log("Total Score: " + TotalScore);
            Debug.Log("Level Score: " + LevelScore);
        }
    }
}