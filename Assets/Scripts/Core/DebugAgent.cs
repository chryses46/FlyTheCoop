using FlyTheCoop.Player;
using FlyTheCoop.UI;
using UnityEngine;

namespace FlyTheCoop.Core
{
    public class DebugAgent: MonoBehaviour
    {
#region TypeReferences
        LevelLoader levelLoader;
        EggManager eggManager;
        PlayerPrefConfig ppc;
#endregion
#region Startup
        void Start()
        {
            levelLoader = GetComponent<LevelLoader>();
            eggManager = GetComponent<EggManager>();
            ppc = GetComponent<PlayerPrefConfig>();
        }
        void Update()
        {
            if(Debug.isDebugBuild)
            {
                RespondToDebugKeys();    
            }
        }
#endregion
        private void RespondToDebugKeys() // Each of these should call a specific method.
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CallNextScene();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                DisableCollisions();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                AddToEggCount();
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                SetNormalModeComplete();
            }
        }

        private void SetNormalModeComplete()
        {
            if(ppc.IsNormalModeCompleted())
            {
                PlayerPrefs.SetString("NormalModeCompleted", "false");    
            }
            else
            {
                ppc.NormalModeCompleted();
            }
            
        }

        private void AddToEggCount()
        {
            HUDController hud = FindObjectOfType<HUDController>();

            eggManager.EggCollected();
        }
        private void DisableCollisions()
        {
            Chicken chicken = FindObjectOfType<Chicken>();
            if (chicken)
            {
                chicken.collisionsDisabled = !chicken.collisionsDisabled;
            }
        }
        private void CallNextScene()
        {
            levelLoader.NextScene();
        }
    }
}