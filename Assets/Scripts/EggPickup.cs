using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class EggPickup : MonoBehaviour
{
    HUDController hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = FindObjectOfType<HUDController>();
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
