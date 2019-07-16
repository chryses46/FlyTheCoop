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
            levelLoader.StartGame(LevelLoader.GameMode.Hard);
        }
        private void SetNormalMode()
        {
            levelLoader.StartGame(LevelLoader.GameMode.Normal);
        }
    }
}
