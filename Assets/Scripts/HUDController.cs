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

        [SerializeField] int requiredModifier = 3; //for HardMode: a higher number means more eggs required to move forwar

        PlayerPrefConfig ppc;
        AudioSource audioSource;
        LevelLoader levelLoader;

        //private int TotalEggsRequired { get; set; } // Increasing value for HardMode play

        const int initialRequired = 3; // const to base TotalEgg formula around

        public GameObject FinishLine;

        private int _requiredEggs;


        public int RequiredEggs 
        { 
            get 
            {
                return _requiredEggs;
            } 
            set
            {
                _requiredEggs = value;
            } 
        }

        private int LevelScore { get; set; }
        public int TotalScore { get; private set; }

        void OnEnable()
        {
            ppc = FindObjectOfType<PlayerPrefConfig>();
            audioSource = GetComponent<AudioSource>();
            levelLoader = FindObjectOfType<LevelLoader>();

        }

        public void EggCollected()
        {
            audioSource.PlayOneShot(collectEgg);
            
            if(levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard)
            {
                TotalScore += 1;
            }
            else
            {
                LevelScore += 1;
            }
            
            UpdateFinishLine();
            UpdateEggCountText();
        }

        public void UpdateFinishLine(GameObject finish)
        { 

            FinishLine = finish;

            FinishLine.tag = "Untagged"; // Disables the FinishLine
            Debug.Log("FinishLine locked: " + FinishLine.tag);
            
        }

        public void UpdateFinishLine()
        {
            if(levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard && TotalScore >= _requiredEggs)
            {
                FinishLine.tag = "Finish";
                Debug.Log("FinishLine unlocked: " + FinishLine.tag);
            }
            else if(LevelScore >= _requiredEggs)
            {
                FinishLine.tag = "Finish";
                Debug.Log("FinishLine unlocked: " + FinishLine.tag);
            }
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

            if(levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard)
            {
                UpdateRequiredHardModeEggsCount(0);
            } 
            ResetLevelScore();
            UpdateEggCountText();
        }

        public void ResetLevelScore()
        {
            LevelScore = 0;
            UpdateEggCountText();
            
        }

        public void UpdateEggCountText()
        {   
            if(levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard)
            {
                eggCountText.text = (LevelScore + TotalScore) + "/" + _requiredEggs;
            }
            else
            {
                eggCountText.text = LevelScore + "/" + _requiredEggs;
            }
            
        }

        public void UpdateRequiredHardModeEggsCount(int currentLevelIndex)
        {
            if(currentLevelIndex >= 0)
            {
                RequiredEggs = initialRequired + currentLevelIndex * requiredModifier;
                UpdateEggCountText();
            }

        }

        

        
    }
}