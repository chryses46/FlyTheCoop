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
            mainMenu.onClick.AddListener(LoadMainMenu);
            levelLoader = GetComponent<LevelLoader>();

        }

        public void LoadMainMenu()
        {
            Debug.Log("Clicked");
            SceneManager.LoadScene(0);
            if(levelLoader.CurrentGameState == LevelLoader.GameState.Pause)
            {
                if(GameObject.Find("PauseScreen").activeSelf == true)
                {
                    GameObject.Find("PauseScreen").SetActive(false);
                }
            }
        
        }
    }
}
