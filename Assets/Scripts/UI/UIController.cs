using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlyTheCoop.Core;

namespace FlyTheCoop.UI
{
    public class UIController : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] GameObject mainSpalsh;
        [SerializeField] GameObject pauseScreen;
        [SerializeField] Button pauseMainMenuButton;
        [SerializeField] Button pauseControlsButton;
        public GameObject controlsScreen;
        [SerializeField] Button controlsBackButton;
        [SerializeField] GameObject hUD;
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
            eggManager = GetComponent<EggManager>();
            levelLoader = GetComponent<LevelLoader>();
            state = GetComponent<StateController>();
            controlsBackButton.onClick.AddListener(HideControlScreen);
            pauseControlsButton.onClick.AddListener(DisplayControlScreen);
            pauseMainMenuButton.onClick.AddListener(levelLoader.LoadMainMenu);
        }

        void Update()
        {
            PauseGame();
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
        public void PauseGame()
        {
            if(controlsScreen.activeSelf != true & hUD.activeSelf == true)
            {
                if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    if(state.CurrentGameState == StateController.GameState.Pause)
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
                    mainSpalsh = FindObjectOfType<MainMenuUI>().gameObject;
                }

                mainSpalsh.SetActive(false);
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
                mainSpalsh.SetActive(true);
            }
        }
    }
}
