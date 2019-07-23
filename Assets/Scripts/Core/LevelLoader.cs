using System.Collections;
using FlyTheCoop.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlyTheCoop.Core
{
    public class LevelLoader : MonoBehaviour
    {
#region PublicProperties
        public float levelLoadDelay = 2f;
#endregion
#region TypeReferences
        MusicPlayer music;
        //HUDController hud;
        StateController state;
        UIController ui;
#endregion
#region Startup
        // void OnEnable()
        // {
        //     SceneManager.sceneLoaded += OnSceneLoaded;
        // }

        public void Start()
        {
            //AddListeners();
            ThereCanOnlyBeOne();
            music = GetComponent<MusicPlayer>();
            state = GetComponent<StateController>();
            ui = GetComponent<UIController>();
        }
#endregion
#region SceneNavigation
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
            music.PlayIntroMusic();
            ui.HUDEnabled(false);
            state.CurrentGameState = StateController.GameState.Menu;
            ui.PauseScreenControl(); 
        }
        public void LoadModeScene()
        {
            SceneManager.LoadScene("ModeSelect");
            music.PlayIntroMusic();
        }
        public void LoadLevelSelect() //for future release
        {
            SceneManager.LoadScene("LevelSelect");
            music.PlayIntroMusic();
        }
        public void SelectLevel(string name) //for future release
        {
            StartGame(StateController.GameMode.Normal, name);
        }
        public void StartGame(StateController.GameMode mode, string name = null)
        {
            state.CurrentGameState = StateController.GameState.Play;
            
            state.CurrentGameMode = mode;
            
            if(name != null)
            {
                SceneManager.LoadScene(name);
            }
            else
            {
                SceneManager.LoadScene(3);    
            }
            ui.HUDEnabled(true);
            music.PlayGameMusic();
        }
        private void FirstScene()
        {
            SceneManager.LoadScene(3);
        }
        private void ThisScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
        public void NextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
//Eggmanager          hud.UpdateTotalScore();

            //Reaches end of SceceCount (wins game) and goes to WinScreen
            if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 3)
            {
                GameWon();
            }
            else //Loads the next scene
            {
                SceneManager.LoadScene(currentSceneIndex + 1);

                if(state.CurrentGameMode == StateController.GameMode.Hard)
                {
//Eggmanager                    hud.UpdateRequiredHardModeEggsCount(currentSceneIndex - 2);
                }
            }
        }
        private void GameWon()
        {
            Debug.Log("GameWon called");
            SceneManager.LoadScene("WinScreen");
//Eggmanager            hud.UpdateHighScore();
            music.PlayWinMusic();
        }
        public IEnumerator LoadSceneWithDelay(StateController.GameMode mode = StateController.GameMode.Normal, bool winning = true) // SceneLoader
        {
            if(!winning && mode == StateController.GameMode.Hard)
            {
                yield return new WaitForSeconds(levelLoadDelay);
//Eggmanager                hud.ResetLevelScore();
//Eggmanager                hud.ResetScore();
                FirstScene();
            }
            else if(!winning)
            {
                yield return new WaitForSeconds(levelLoadDelay);
//Eggmanager                hud.ResetLevelScore();
                ThisScene();
            }

            if(winning)
            {
                Camera.main.transform.Find("WinFXv2").gameObject.SetActive(true);
                yield return new WaitForSeconds(levelLoadDelay);
                NextScene();
            }
        }
#endregion
        private void ThereCanOnlyBeOne()
        {
            int numLevelLoaders = FindObjectsOfType<LevelLoader>().Length;
        
            if(numLevelLoaders > 1)
            {
                Destroy(gameObject);
            }else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}