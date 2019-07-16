using UnityEngine;

namespace FlyTheCoop.Tools
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
#region Startup
        void Start()
        {
            int these = FindObjectsOfType<DontDestroyOnLoad>().Length;

            if(these > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }   
        }
#endregion
    }
}
