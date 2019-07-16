using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;

namespace FlyTheCoop.UI
{
    public class NewGame : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Button newGame;
#endregion
#region TypeReferences
        LevelLoader levelLoader;
#endregion
#region Startup
        void OnEnable()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            newGame.onClick.AddListener(CallLoadModeScene);
        }
#endregion
        void CallLoadModeScene()
        {
            levelLoader.LoadModeScene();
        }
    }
}
