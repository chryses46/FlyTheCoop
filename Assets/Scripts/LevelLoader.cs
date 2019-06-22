using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Core
{
    public class LevelLoader : MonoBehaviour
    {

// This class is a component of GameObject "System"
        public float levelLoadDelay = 2f;

        #region GameMode
        //Enum for game mode
        public enum GameMode
        {
            Normal,
            Hard
        }
    
        private GameMode _gameMode;

        public GameMode CurrentGameMode
        {
            get{ return _gameMode; }
            set{ _gameMode = value; } 
        }
        #endregion
        #region GameState
        //Enum for gameState

        public enum GameState
        {
            Menu,
            Play,
            Pause
        }

        private GameState _gameState;
        public GameState CurrentGameState
        {
            get{ return _gameState;}
            set{ _gameState = value;}
        }
        #endregion
        [SerializeField] Button newGame;
        [SerializeField] Button controls;
        [SerializeField] Button version;
        public GameObject pauseScreen;

        GameObject mainSpalsh;

        MusicPlayer music;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Start()
        {
            AddListeners();
            ThereCanOnlyBeOne();
            music = GetComponent<MusicPlayer>();
        }

        void Update()
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                HideVersionInfoOnClickAway();
            }
        }

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

        public void PopulateButtons()
        {
            if(!newGame & !controls & !version)
            {
                mainSpalsh = GameObject.Find("MainSplashCanvas");
                newGame = mainSpalsh.transform.Find("New Game").gameObject.GetComponent<Button>();
                controls = mainSpalsh.transform.Find("Controls").gameObject.GetComponent<Button>();
                version = mainSpalsh.transform.Find("Version").gameObject.GetComponent<Button>();
            }

            AddListeners();
        }

        public void AddListeners()
        {
            newGame.onClick.AddListener(LoadModeScene);
            controls.onClick.AddListener(ShowControls);
            version.onClick.AddListener(ShowVersionInfo);
        }

        #region versionInfo
        void HideVersionInfoOnClickAway()
        {
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast( ray,out hit, 10000))
                {
                    string name = hit.collider.transform.name;

                    if(name != "VersionInfo"){
                        
                        HideVersionInfo();
                    }
                }else if(Input.GetKeyDown(KeyCode.Escape))
                {
                    HideVersionInfo();
                }
            }
        }

        public void ShowVersionInfo()
        {
            GameObject versionInfoGo = GameObject.Find("VersionInfo");
            string name = versionInfoGo.name;
            float yStart = versionInfoGo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 1000)
            {
                float yEnd = 0;
               StartCoroutine(MoveObject(versionInfoGo, yStart, yEnd));
                
            }
        }

        public void HideVersionInfo()
        {
            GameObject versionInfoGo = GameObject.Find("VersionInfo");
            string name = versionInfoGo.name;
            float yStart = versionInfoGo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 0)
            {
                float yEnd = 1000;
               StartCoroutine(MoveObject(versionInfoGo, yStart, yEnd));
                
            }
        }
        
        IEnumerator MoveObject(GameObject go, float start, float end)
        {
            RectTransform goRect = go.GetComponent<RectTransform>();
            float t = 0.0f;

            while(t < 1)
            {
                t += Time.deltaTime / .5f;
                goRect.localPosition = new Vector2(0,Mathf.Lerp(start,end,t));
                yield return null;
            }
        }
        #endregion

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
            music.PlayIntroMusic();
            CurrentGameState = GameState.Menu;
            
            if(pauseScreen.activeSelf == true)
            {
                pauseScreen.SetActive(false);
            }   
        }

        public void ShowControls()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadModeScene() // SceneLoader
        {
            SceneManager.LoadScene(2);
            music.PlayIntroMusic();
        }

        public void StartGame()
        {
            CurrentGameState = GameState.Play;
            SceneManager.LoadScene(3);
            music.PlayGameMusic();
        }

        public void GameWon()
        {
            SceneManager.LoadScene("WinScreen");
            music.PlayWinMusic();
        }

        public void PauseScreenControl()
        {

            if(CurrentGameState == GameState.Pause)
            {
                Debug.Log("Gamesate is paused and pause screen called.");
                pauseScreen.SetActive(true);

            }else
            {
                Debug.Log("Gamesate is Play and pause screen hidden.");
                pauseScreen.SetActive(false);
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }

        }

        public IEnumerator LoadSceneWithDelay(GameMode mode = GameMode.Normal, bool winning = true) // SceneLoader
        {
            
            if(!winning && mode == GameMode.Hard)
            {
                yield return new WaitForSeconds(levelLoadDelay);
                FirstScene();
                
            }
            else if(!winning)
            {
                yield return new WaitForSeconds(levelLoadDelay);
                ThisScene();
            }

            if(winning)
            {
                Camera.main.transform.Find("WinFXv2").gameObject.SetActive(true);
                yield return new WaitForSeconds(levelLoadDelay);
                NextScene();
            }
        }

        private void FirstScene()
        {
            SceneManager.LoadScene(3);
        }

        public void NextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            //Reaches end of SceceCount (wins game) and goes to WinScreen
            if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 2)
            {
                GameWon();
            }
            else //Loads the next scene
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }

        private void ThisScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            int loadedSceneIndex = scene.buildIndex;

            if(loadedSceneIndex == 0)
            {
                CurrentGameState = GameState.Menu;
                PopulateButtons();
            }
            
        }
    }
}