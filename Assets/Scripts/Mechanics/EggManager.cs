using UnityEngine;
using FlyTheCoop.Core;
using FlyTheCoop.UI;

namespace FlyTheCoop.Mechanics
{
    public class EggManager : MonoBehaviour
    {
#region PublicProperties
        public int requiredEggs; //requiredEggs in NormalMode
#endregion
#region TypeReferences
        HUDController hud;
        LevelLoader levelLoader;
        StateController state;
#endregion
#region Startup
        void Awake()
        {
            hud = FindObjectOfType<HUDController>();
            levelLoader = FindObjectOfType<LevelLoader>();
            state = FindObjectOfType<StateController>();
        }
        void Start()
        {
            SetRequiredEggs();
        }
#endregion
        private void SetRequiredEggs()
        {
            if(state.CurrentGameMode == StateController.GameMode.Normal)
            {
                hud.RequiredEggs = requiredEggs;
                hud.UpdateEggCountText();
            }
        }
    }
}
