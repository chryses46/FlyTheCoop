using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;

namespace FlyTheCoop.UI
{
    public class WinScreenUI : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Button mainMenu;
        [SerializeField] Button newGame;        
        [SerializeField] Text normalModeText;
        [SerializeField] List<Text> hardModeUIText;
        [SerializeField] Text eggsCollected;
        [SerializeField] Text mostEggsCollected;
#endregion
#region TypeReferences
        LevelLoader levelLoader;
        EggManager eggManager;
        UIController ui;
        PlayerPrefConfig ppc;
        StateController state;
#endregion
#region Startup
        void Start()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            eggManager = FindObjectOfType<EggManager>();
            ui = FindObjectOfType<UIController>();
            ppc = FindObjectOfType<PlayerPrefConfig>();
            state = FindObjectOfType<StateController>();
            DetermineModeText();
            SetEggsCollectedText();
            SetMostEggsText();
            AddListeners();
        }
        private void AddListeners()
        {
            newGame.onClick.AddListener(levelLoader.LoadModeScene);
            mainMenu.onClick.AddListener(levelLoader.LoadMainMenu);
        }
#endregion
#region WinScreenUIMethods
        private void DetermineModeText()
        {
            ui.HUDEnabled(false);

            if (state.CurrentGameMode == StateController.GameMode.Hard)
            {
                EnableHardModeUIText();
            }
            else
            {
                EnableNormalModeText();
                ppc.NormalModeCompleted();
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
            eggsCollected.text = "Eggs Collected: " + eggManager.TotalScore;
        }
        private void SetMostEggsText()
        {
            mostEggsCollected.text = "Most Eggs Collected: " + ppc.HighScore;
        }
#endregion
    }
}
