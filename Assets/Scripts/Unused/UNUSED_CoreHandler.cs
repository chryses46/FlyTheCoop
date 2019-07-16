using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyTheCoop.Core
{
    public class CoreHandler : MonoBehaviour
    {
        private void Awake()
        {
            int numCoreHandlers = FindObjectsOfType<CoreHandler>().Length;
        
            if(numCoreHandlers > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    } 
}

