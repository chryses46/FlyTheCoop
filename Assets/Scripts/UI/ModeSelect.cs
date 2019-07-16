using FlyTheCoop.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class ModeSelect : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Button normalButton;
        [SerializeField] Button hardButton;
#endregion
#region TypeReferences
        LevelLoader levelLoader;
#endregion
#region Startup
        void Start()
        {
            normalButton.onClick.AddListener(SetNormalMode);
            hardButton.onClick.AddListener(SetHardMode);
            
            levelLoader = FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>();
        }
#endregion

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
    }
}
