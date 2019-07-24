using System;
using UnityEngine;

namespace FlyTheCoop.Core
{
    public class PlayerPrefConfig : MonoBehaviour
    {
#region PublicProperties
        public int HighScore { get; private set; }
        public int EggBasket { get; private set; }
#endregion
#region Startup
        void Awake()
        {
            ManageHighScore();
            ManageEggBasket();
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
        public void ManageEggBasket(int eggCount = 0)
        {
            int currentEggBasketCount = PlayerPrefs.GetInt("EggBasket");

            if(currentEggBasketCount > 0)
            {
                EggBasket = currentEggBasketCount;
            }

            if(eggCount > 0)
            {
                int updatedEggBasketCount = currentEggBasketCount + eggCount;

                PlayerPrefs.SetInt("EggBasket", updatedEggBasketCount);
                EggBasket = updatedEggBasketCount;
            }
        }
        public bool IsNormalModeCompleted()
        {
            string currentNormalModeCompletionStatus = PlayerPrefs.GetString("NormalModeCompleted");

            if(currentNormalModeCompletionStatus == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void NormalModeCompleted()
        {
            PlayerPrefs.SetString("NormalModeCompleted", "true");
        }

    }
}
