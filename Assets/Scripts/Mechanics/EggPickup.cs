using UnityEngine;
using FlyTheCoop.Core;

namespace FlyTheCoop.Mechanics
{
    [DisallowMultipleComponent]
    public class EggPickup : MonoBehaviour
    {
#region PublicProperties
        public bool isHardModeEgg;
#endregion        
#region TypeReferences
        EggManager eggManager;
        StateController state;
#endregion
#region Startup
        void Start()
        {
            eggManager = FindObjectOfType<EggManager>();
            state = FindObjectOfType<StateController>();
            IsThisHardMode();
        }
#endregion
        private void IsThisHardMode()
        {
            if(isHardModeEgg && state.CurrentGameMode != StateController.GameMode.Hard)
            {
                gameObject.SetActive(false);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                eggManager.EggCollected();
                Destroy(this.gameObject);
            }
        }
    }
}
