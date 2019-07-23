using FlyTheCoop.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class MainMenu : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Button mainMenu;
#endregion
#region TypeReferences
        LevelLoader levelLoader;
#endregion
#region Startup
        public void OnEnable()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            mainMenu.onClick.AddListener(CallLoadMainMenu);
        }
#endregion
        private void CallLoadMainMenu()
        {
            levelLoader.LoadMainMenu();
        }
    }
}
