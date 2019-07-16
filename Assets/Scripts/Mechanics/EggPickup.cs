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
        LevelLoader levelLoader;
#endregion
#region Startup
        void Start()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            hud = FindObjectOfType<HUDController>();
            IsThisHardMode();
        }
#endregion
        private void IsThisHardMode()
        {
            if(isHardModeEgg && levelLoader.CurrentGameMode != LevelLoader.GameMode.Hard)
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
