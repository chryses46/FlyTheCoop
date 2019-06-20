using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class ModeSelect : MonoBehaviour
    {
        
        [SerializeField] Button normalButton;
        [SerializeField] Button hardButton;

        LevelLoader levelLoader;

        void Start()
        {
            normalButton.onClick.AddListener(SetNormalMode);
            hardButton.onClick.AddListener(SetHardMode);
            levelLoader = FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>();
        }

        private void SetHardMode()
        {
            levelLoader.CurrentGameMode = LevelLoader.GameMode.Hard;
            levelLoader.StartGame();
        }

        private void SetNormalMode()
        {
            levelLoader.CurrentGameMode = LevelLoader.GameMode.Normal;
            levelLoader.StartGame();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
