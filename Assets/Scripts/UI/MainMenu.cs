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
            levelLoader = GameObject.Find("GameController").GetComponent<LevelLoader>();
            mainMenu.onClick.AddListener(CallMainMenu);
        }
#endregion
        private void CallMainMenu()
        {
            levelLoader.LoadMainMenu();
        }
    }
}
