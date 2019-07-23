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


        void Awake()
        {
            ui = FindObjectOfType<UIController>();
            levelLoader = FindObjectOfType<LevelLoader>();
            AddListeners();
        }

        private void AddListeners()
        {
            newGame.onClick.AddListener(levelLoader.LoadModeScene);
            controls.onClick.AddListener(ui.DisplayControlScreen);
            levelSelect.onClick.AddListener(levelLoader.LoadLevelSelect);
        }
    }
}
