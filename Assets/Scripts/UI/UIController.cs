using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlyTheCoop.Core;

namespace FlyTheCoop.UI
{
    public class UIController : MonoBehaviour
    {
        public GameObject mainSpalsh;
        public GameObject pauseScreen;
        [SerializeField] Button pauseControlsButton;
        public GameObject controlsScreen;
        [SerializeField] Button controlsBackButton;
        public GameObject hUD;
        HUDController hud;
        LevelLoader levelLoader;
        StateController state;
        void OnEnable()
        {
            levelLoader = GetComponent<LevelLoader>();
            state = GetComponent<StateController>();
            controlsBackButton.onClick.AddListener(HideControlScreen);
            pauseControlsButton.onClick.AddListener(DisplayControlScreen);
        }
         public void HUDEnabled(bool enabled)
        {
            if(enabled)
            {
                hUD.SetActive(true);
                hud = hUD.GetComponent<HUDController>();
                hud.ResetScore();
            }
            else
            {
                hUD.SetActive(false);
            }
        }
        public void PauseScreenControl(StateController.GameState gameState)
        {
            if(gameState == StateController.GameState.Pause)
            {
                pauseScreen.SetActive(true);

            }else
            {
                pauseScreen.SetActive(false);
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
        public void PauseScreenControl(bool isActive)
        {
            pauseScreen.SetActive(isActive);
        }
        public void DisplayControlScreen()
        {
            
            if(state.CurrentGameState == StateController.GameState.Pause)
            {
                pauseScreen.SetActive(false);
            }
            else if(state.CurrentGameState == StateController.GameState.Menu)
            {
                mainSpalsh.SetActive(false);
            }

            controlsScreen.SetActive(true);
        }
        public void HideControlScreen()
        {   
            controlsScreen.SetActive(false);

            if(state.CurrentGameState == StateController.GameState.Pause)
            {
                pauseScreen.SetActive(true);
            }
            else if(state.CurrentGameState == StateController.GameState.Menu)
            {
                mainSpalsh.SetActive(true);
            }
        }
    }
}
