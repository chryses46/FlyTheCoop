using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game.Core
{
    public class DebugAgent: MonoBehaviour
    {
// This class is a component of GameObject "System"
        public bool collisionsDisabled = false;
        LevelLoader levelLoader;
        public void RespondToDebugKeys() //Core
        {
            if (Input.GetKeyDown(KeyCode.L)) 
            {
                levelLoader.LoadNextScene();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                collisionsDisabled = !collisionsDisabled;
            }
        }
    }
}