using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;

namespace FlyTheCoop.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] Button newGame;
        [SerializeField] Button controls;
        [SerializeField] Button levelSelect;

        UIController ui;
        LevelLoader levelLoader;


        void OnEnable()
        {
            ui = FindObjectOfType<UIController>();
            levelLoader = FindObjectOfType<LevelLoader>();
            AddListeners();
        }

        private void AddListeners()
        {
            newGame.onClick.AddListener(LoadModeScene);
            controls.onClick.AddListener(ControlScreen);
            levelSelect.onClick.AddListener(LoadLevelSelect);
        }

        private void LoadModeScene()
        {
            levelLoader.LoadModeScene();
        }
        private void ControlScreen()
        {
            ui.DisplayControlScreen();
        }
        private void LoadLevelSelect()
        {
            levelLoader.LoadLevelSelect();
        }
    }
}
