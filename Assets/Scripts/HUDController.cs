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

        PlayerPrefConfig ppc;

        private int LevelScore { get; set; }
        public int TotalScore { get; private set; }

        void OnEnable()
        {
            ppc = FindObjectOfType<PlayerPrefConfig>();
        }

        public void EggCollected()
        {
            Debug.Log("Egg Collected.");
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
    }
}