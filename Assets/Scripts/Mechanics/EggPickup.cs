using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlyTheCoop.Core;
using FlyTheCoop.UI;

namespace FlyTheCoop.Mechanics
{
    [DisallowMultipleComponent]
    public class EggPickup : MonoBehaviour
    {
#region PublicProperties
        public bool isHardModeEgg;
#endregion        
#region TypeReferences
        HUDController hud;
        LevelLoader LevelLoader;
#endregion
#region Startup
        void Start()
        {
            LevelLoader = FindObjectOfType<LevelLoader>();
            hud = FindObjectOfType<HUDController>();
            IsThisHardMode(LevelLoader.CurrentGameMode);
        }
#endregion
        private void IsThisHardMode(LevelLoader.GameMode mode)
        {
            if(isHardModeEgg && mode != LevelLoader.GameMode.Hard)
            {
                gameObject.SetActive(false);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                hud.EggCollected();
                Destroy(this.gameObject);
            }
        }
    }
}
