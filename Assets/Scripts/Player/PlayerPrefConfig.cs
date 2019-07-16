﻿using UnityEngine;

namespace FlyTheCoop.Player
{
    public class PlayerPrefConfig : MonoBehaviour
    {
#region PublicProperties
        public int HighScore { get; private set; }
#endregion
#region Startup
        void OnEnable()
        {
            ManageHighScore();
        }
#endregion
        public void ManageHighScore(int sessionScore = 0)
        {
            int prevHighScore = PlayerPrefs.GetInt("HighScore");
                
            if(prevHighScore != 0 & prevHighScore > sessionScore)
            {
                HighScore = prevHighScore;
            }
            else if(sessionScore > prevHighScore)
            {
                HighScore = sessionScore;
                PlayerPrefs.SetInt("HighScore", sessionScore);
            }
        }
    }
}