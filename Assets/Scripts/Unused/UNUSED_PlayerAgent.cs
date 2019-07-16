using UnityEngine;
using UnityEngine.SceneManagement;
using FlyTheCoop.Core;

namespace FlyTheCoopControl
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