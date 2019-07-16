using UnityEngine;
using FlyTheCoop.UI;

namespace FlyTheCoop.Mechanics
{
    public class FinishLine : MonoBehaviour
    {
#region TypeReferences
        HUDController hud;
#endregion
#region Startup
        void OnEnable()
        {
            hud = FindObjectOfType<HUDController>();
        }
        void Start()
        {
            hud.UpdateFinishLine(gameObject);
        }
#endregion
    }
}
