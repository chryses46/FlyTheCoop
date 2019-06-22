 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] Button mainMenu;
    
        LevelLoader levelLoader;
        public void OnEnable()
        {
            levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            mainMenu.onClick.AddListener(CallMainMenu);

        }

        private void CallMainMenu()
        {
            levelLoader.LoadMainMenu();
        }
    }
}
