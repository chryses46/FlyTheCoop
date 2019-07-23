using UnityEngine;
using FlyTheCoop.Core;

namespace FlyTheCoop.Mechanics
{
    public class Exit : MonoBehaviour
    {
#region PublicProperties
        public int requiredEggs; //requiredEggs in NormalMode
#endregion
#region TypeReferences
        EggManager eggManager;
#endregion
#region Startup
        void OnEnable()
        {
            eggManager = FindObjectOfType<EggManager>();
        }
        void Start()
        {
            eggManager.UpdateExit(gameObject, requiredEggs);
        }
#endregion
    }
}
