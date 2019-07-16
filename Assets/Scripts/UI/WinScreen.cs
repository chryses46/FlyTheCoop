using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;
using FlyTheCoop.Player;

namespace FlyTheCoop.UI
{
    public class WinScreen : MonoBehaviour
    {
#region PublicProperties        
        [SerializeField] Text normalModeText;
        [SerializeField] List<Text> hardModeUIText;
        [SerializeField] Text eggsCollected;
        [SerializeField] Text mostEggsCollected;
#endregion
#region TypeReferences
        LevelLoader levelLoader;
        HUDController hud;
        PlayerPrefConfig ppc;
#endregion
#region Startup
        void Start()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            hud = FindObjectOfType<HUDController>();
            ppc = FindObjectOfType<PlayerPrefConfig>();
            DetermineText();
            SetEggsCollectedText();
            SetMostEggsText();
        }
#endregion

        private void DetermineText()
        {
            levelLoader.HUDEnabled(false);

            if (levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard)
            {
                EnableHardModeUIText();
            }
            else
            {
                EnableNormalModeText();
            }
        }

        private void EnableNormalModeText()
        {
            normalModeText.gameObject.SetActive(true);
        }

        private void EnableHardModeUIText()
        {
            for(int i=0; i< hardModeUIText.Count; i++)
            {
                hardModeUIText[i].gameObject.SetActive(true);
            }
        }

        private void SetEggsCollectedText()
        {
            eggsCollected.text = "Eggs Collected: " + hud.TotalScore;
        }

        private void SetMostEggsText()
        {
            mostEggsCollected.text = "Most Eggs Collected: " + ppc.HighScore;
        }
    }
}
