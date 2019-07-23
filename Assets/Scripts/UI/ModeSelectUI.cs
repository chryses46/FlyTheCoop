using FlyTheCoop.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class ModeSelectUI : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Button normalButton;
        [SerializeField] Button hardButton;
        [SerializeField] Button mainMenu;
#endregion
#region TypeReferences
        LevelLoader levelLoader;
#endregion
#region Startup
        void Start()
        {
            normalButton.onClick.AddListener(SetNormalMode);
            hardButton.onClick.AddListener(SetHardMode);
            mainMenu.onClick.AddListener(LoadMainMenu);
            
            levelLoader = FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>();
        }
#endregion

        private void SetHardMode()
        {
            levelLoader.StartGame(StateController.GameMode.Hard);
        }
        private void SetNormalMode()
        {
            levelLoader.StartGame(StateController.GameMode.Normal);
        }
        private void LoadMainMenu()
        {
            levelLoader.LoadMainMenu();
        }
    }
}
