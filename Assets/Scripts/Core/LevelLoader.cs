using System.Collections;
using FlyTheCoop.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlyTheCoop.Core
{
    public class LevelLoader : MonoBehaviour
    {
#region Enums
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
#endregion
#region PublicProperties
        public float levelLoadDelay = 2f;
        [SerializeField] Button newGame;
        [SerializeField] Button controls;
        [SerializeField] Button version;
        [SerializeField] Button levelSelect;
        public GameObject pauseScreen;
        public GameObject hudGo;
        GameObject mainSpalsh;
#endregion
#region TypeReferences
        MusicPlayer music;
        HUDController hud;
#endregion
#region Startup
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
#endregion
#region ManageButtons
        private void PopulateButtons()
        {
            if(!newGame & !controls & !version)
            {
                mainSpalsh = GameObject.Find("MainSplashCanvas");
                newGame = mainSpalsh.transform.Find("New Game").gameObject.GetComponent<Button>();
                controls = mainSpalsh.transform.Find("Controls").gameObject.GetComponent<Button>();
                version = mainSpalsh.transform.Find("Version").gameObject.GetComponent<Button>();
                levelSelect = mainSpalsh.transform.Find("LevelSelect").gameObject.GetComponent<Button>();
            }

            AddListeners();
        }

        private void AddListeners()
        {
            newGame.onClick.AddListener(LoadModeScene);
            controls.onClick.AddListener(ShowControls);
            version.onClick.AddListener(ShowVersionInfo);
            levelSelect.onClick.AddListener(LoadLevelSelect);
        }
#endregion
#region VersionInfo
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

        private void ShowVersionInfo()
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

        private void HideVersionInfo()
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
#region SceneNavigation
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
            music.PlayIntroMusic();
            HUDEnabled(false);
            CurrentGameState = GameState.Menu;
            
            if(pauseScreen.activeSelf == true)
            {
                pauseScreen.SetActive(false);
            }   
        }
        private void ShowControls()
        {
            SceneManager.LoadScene("Controls");
        }
        public void LoadModeScene()
        {
            SceneManager.LoadScene("ModeSelect");
            music.PlayIntroMusic();
        }
        private void LoadLevelSelect() //for future release
        {
            SceneManager.LoadScene("LevelSelect");
            music.PlayIntroMusic();
        }
        public void SelectLevel(string name) //for future release
        {
            StartGame(GameMode.Normal, name);
        }
        public void StartGame(GameMode mode, string name = null)
        {
            CurrentGameState = GameState.Play;
            
            CurrentGameMode = mode;
            
            if(name != null)
            {
                SceneManager.LoadScene(name);
            }
            else
            {
                SceneManager.LoadScene(3);    
            }
            HUDEnabled(true);
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
            
            hud.UpdateTotalScore();

            //Reaches end of SceceCount (wins game) and goes to WinScreen
            if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 3)
            {
                GameWon();
            }
            else //Loads the next scene
            {
                SceneManager.LoadScene(currentSceneIndex + 1);

                if(CurrentGameMode == GameMode.Hard)
                {
                    hud.UpdateRequiredHardModeEggsCount(currentSceneIndex - 2);
                }
            }
        }
        private void GameWon()
        {
            Debug.Log("GameWon called");
            SceneManager.LoadScene("WinScreen");
            hud.UpdateHighScore();
            music.PlayWinMusic();
        }
        public IEnumerator LoadSceneWithDelay(GameMode mode = GameMode.Normal, bool winning = true) // SceneLoader
        {
            if(!winning && mode == GameMode.Hard)
            {
                yield return new WaitForSeconds(levelLoadDelay);
                hud.ResetLevelScore();
                hud.ResetScore();
                FirstScene();
            }
            else if(!winning)
            {
                yield return new WaitForSeconds(levelLoadDelay);
                hud.ResetLevelScore();
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
        public void HUDEnabled(bool enabled)
        {
            if(enabled)
            {
                hudGo.SetActive(true);
                hud = hudGo.GetComponent<HUDController>();
                hud.ResetScore();
            }
            else
            {
                hudGo.SetActive(false);
            }
        }
        public void PauseScreenControl()
        {
            if(CurrentGameState == GameState.Pause)
            {
                pauseScreen.SetActive(true);

            }else
            {
                pauseScreen.SetActive(false);
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
#endregion
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            int loadedSceneIndex = scene.buildIndex;

            if(loadedSceneIndex == 0)
            {
                CurrentGameState = GameState.Menu;
                PopulateButtons();
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
    }
}