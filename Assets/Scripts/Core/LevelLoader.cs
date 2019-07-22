using System.Collections;
using FlyTheCoop.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlyTheCoop.Core
{
    public class LevelLoader : MonoBehaviour
    {
#region PublicProperties
        public float levelLoadDelay = 2f;
        // [SerializeField] Button newGame;
        // [SerializeField] Button controls;
    
        // [SerializeField] Button levelSelect;
        // public GameObject pauseScreen;
        // public GameObject hudGo;
        // GameObject mainSpalsh;
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

        void Update()
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                //HideVersionInfoOnClickAway();
            }
        }
#endregion
#region ManageButtons
        // private void PopulateButtons()
        // {
        //     if(!newGame & !controls)
        //     {
        //         mainSpalsh = GameObject.Find("MainSplashCanvas");
        //         newGame = mainSpalsh.transform.Find("New Game").gameObject.GetComponent<Button>();
        //         controls = mainSpalsh.transform.Find("Controls").gameObject.GetComponent<Button>();
        //         levelSelect = mainSpalsh.transform.Find("LevelSelect").gameObject.GetComponent<Button>();
        //     }

        //     AddListeners();
        // }

        // private void AddListeners()
        // {
        //     newGame.onClick.AddListener(LoadModeScene);
        //     controls.onClick.AddListener(ShowControls);
        //     levelSelect.onClick.AddListener(LoadLevelSelect);
        // }

        // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        // {
        //     int loadedSceneIndex = scene.buildIndex;

        //     if(loadedSceneIndex == 0)
        //     {
        //         StateController sc;
        //         sc = GetComponent<StateController>();
        //         sc.CurrentGameState = StateController.GameState.Menu;
        //         PopulateButtons();
        //     }
        // }

#endregion
#region SceneNavigation
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
            music.PlayIntroMusic();
            ui.HUDEnabled(false);
            state.CurrentGameState = StateController.GameState.Menu;
            
            ui.PauseScreenControl(false);

            // if(ui.pauseScreen.activeSelf == true)
            // {
            //     ui.pauseScreen.SetActive(false);
            // }   
        }
        // public void LoadControls()
        // {
        //     SceneManager.LoadScene("Controls");
        // }
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
#region UIControl
        // public void HUDEnabled(bool enabled)
        // {
        //     if(enabled)
        //     {
        //         hudGo.SetActive(true);
        //         hud = hudGo.GetComponent<HUDController>();
        //         hud.ResetScore();
        //     }
        //     else
        //     {
        //         hudGo.SetActive(false);
        //     }
        // }
        // public void PauseScreenControl()
        // {
        //     if(state.CurrentGameState == StateController.GameState.Pause)
        //     {
        //         pauseScreen.SetActive(true);

        //     }else
        //     {
        //         pauseScreen.SetActive(false);
        //         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //         SceneManager.LoadScene(currentSceneIndex);
        //     }
        // }
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