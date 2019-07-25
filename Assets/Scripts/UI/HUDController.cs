using FlyTheCoop.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class HUDController : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Text eggCountText;
        [SerializeField] StateController state;
        [SerializeField] EggManager eggManager;
        [SerializeField] Button pauseButton;
#endregion
#region TypeReferences
        UIController ui;
#endregion
#region Startup
        void Awake()
        {
            ui = FindObjectOfType<UIController>();
        }
        void Start()
        {
            AddListeners();
        }
        private void AddListeners()
        {
            pauseButton.onClick.AddListener(ui.PauseGame);
        }
        #endregion
        #region UIControl
        public void UpdateEggCountText()
        {   
            if(state.CurrentGameMode == StateController.GameMode.Hard)
            {
                eggCountText.text = (eggManager.LevelScore + eggManager.TotalScore) + "/" + eggManager.RequiredEggs;
            }
            else
            {
                eggCountText.text = eggManager.LevelScore + "/" + eggManager.RequiredEggs;
            }
        }
#endregion
    }
}