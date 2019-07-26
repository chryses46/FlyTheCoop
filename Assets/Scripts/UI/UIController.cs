﻿using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;
using System;

namespace FlyTheCoop.UI
{
    public class UIController : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] MainMenuUI mainSpalsh;
        [SerializeField] GameObject pauseScreen;
        [SerializeField] Button pausePlayButton;
        [SerializeField] Button pauseMainMenuButton;
        [SerializeField] Button pauseControlsButton;
        [SerializeField] GameObject confirmMainMenuMessage;
        [SerializeField] Button confirmMessageYesButton;
        [SerializeField] Button confirmMessageCancelButton;
        public GameObject controlsScreen;
        [SerializeField] Button controlsBackButton;
        [SerializeField] GameObject hUD;
        public GameObject noticeWindow;
        [SerializeField] Text noticeText;
        [SerializeField] Button closeNoticeWindowButton;
#endregion
#region TypeReferences
        HUDController hud;
        EggManager eggManager;
        LevelLoader levelLoader;
        StateController state;
#endregion
#region StartUp
        void OnEnable()
        {
            Screen.autorotateToLandscapeLeft = true;

            eggManager = GetComponent<EggManager>();
            levelLoader = GetComponent<LevelLoader>();
            state = GetComponent<StateController>();
            AddListeners();
        }
        private void AddListeners()
        {
            controlsBackButton.onClick.AddListener(HideControlScreen);
            pausePlayButton.onClick.AddListener(PauseGame);
            pauseControlsButton.onClick.AddListener(DisplayControlScreen);
            pauseMainMenuButton.onClick.AddListener(ConfirmMainMenu);
        }
        void Update()
        {
            PauseInteract();
        }
#endregion
        public void HUDEnabled(bool enabled)
        {
            if(enabled)
            {
                hUD.SetActive(true);
                eggManager.ResetScore();
            }
            else
            {
                hUD.SetActive(false);
            }
        }
        public void PauseInteract()
        {
            if(controlsScreen.activeSelf != true & state.CurrentGameState != StateController.GameState.Menu)
            {
                if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    PauseGame();
                }
            }
        }
        public void PauseGame()
        {
            if (state.CurrentGameState == StateController.GameState.Pause)
            {
                state.CurrentGameState = StateController.GameState.Play;
                PauseScreenControl();
                Time.timeScale = 1;
            }
            else
            {
                state.CurrentGameState = StateController.GameState.Pause;
                Time.timeScale = 0;
                PauseScreenControl();
            }
        }
        public void PauseScreenControl()
        {
            if(state.CurrentGameState == StateController.GameState.Pause)
            {
                pauseScreen.SetActive(true);
            }
            else
            {   
                if(confirmMainMenuMessage.activeSelf)
                {
                    CloseConfirmMainMenuWindow();
                }

                pauseScreen.SetActive(false);
            }
        }
        public void DisplayControlScreen()
        {
            
            if(state.CurrentGameState == StateController.GameState.Pause)
            {
                pauseScreen.SetActive(false);
            }
            else if(state.CurrentGameState == StateController.GameState.Menu)
            {
                if(!mainSpalsh)
                {
                    mainSpalsh = FindObjectOfType<MainMenuUI>();
                }
                if(noticeWindow.activeSelf == true)
                {
                    CloseNoticeWindow();
                }
                mainSpalsh.HideVersionInfo();
                mainSpalsh.gameObject.SetActive(false);
                
                
            }

            if(confirmMainMenuMessage.activeSelf)
            {
                CloseConfirmMainMenuWindow();
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
                mainSpalsh.gameObject.SetActive(true);
            }
        }
        private void ConfirmMainMenu()
        {
            confirmMainMenuMessage.SetActive(true);
            confirmMessageYesButton.onClick.AddListener(GoToMainMenu);
            confirmMessageCancelButton.onClick.AddListener(CloseConfirmMainMenuWindow);
        }
        private void GoToMainMenu()
        {
            levelLoader.LoadMainMenu();
            Time.timeScale = 1;
            confirmMainMenuMessage.SetActive(false);
            pauseScreen.SetActive(false);
        }
        private void CloseConfirmMainMenuWindow()
        {
            confirmMainMenuMessage.SetActive(false);
        }
        public void CallNoticeWindow(string message)
        {
            noticeWindow.SetActive(true);
            closeNoticeWindowButton.onClick.AddListener(CloseNoticeWindow);
            noticeText.text = message;
        }
        public void CloseNoticeWindow()
        {
            noticeWindow.SetActive(false);
            noticeText.text = null;
        }
    }
}
