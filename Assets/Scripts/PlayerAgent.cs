using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Core;

namespace Game.Control
{
    public class PlayerAgent : MonoBehaviour
    {
// This class attaches to the Player (Chicken).
        LevelLoader levelLoader;
        EffectsFactory effectsFactory;

        public void Start()
        {
            //Core Components
            levelLoader = FindObjectOfType<LevelLoader>();
            effectsFactory = FindObjectOfType<EffectsFactory>();
        }
       
    }
}