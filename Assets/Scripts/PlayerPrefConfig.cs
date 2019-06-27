using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{

    public class PlayerPrefConfig : MonoBehaviour
    {
        public int HighScore { get; private set; }

        void OnEnable()
        {
            ManageHighScore();
        }

        

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
