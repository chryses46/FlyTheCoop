using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game.Core
{
    public class DebugAgent: MonoBehaviour
    {
// This class is a component of GameObject "System"
        LevelLoader levelLoader;
        HUDController hud;
        Chicken chicken;

        void Start()
        {
            levelLoader = GetComponent<LevelLoader>();

        }

        void Update()
        {
            if(Debug.isDebugBuild)
            {
                RespondToDebugKeys();    
            }
            
        }

        private void RespondToDebugKeys()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                levelLoader.NextScene();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                chicken = FindObjectOfType<Chicken>();
                if(chicken)
                {
                    chicken.collisionsDisabled = !chicken.collisionsDisabled;
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if(!hud)
                {
                    hud = FindObjectOfType<HUDController>();
                }
                
                hud.EggCollected();
            }
        }
    }
}