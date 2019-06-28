using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class EggPickup : MonoBehaviour
{
    HUDController hud;
    LevelLoader LevelLoader;


    public bool isHardModeEgg;

    

    void Start()
    {
        LevelLoader = FindObjectOfType<LevelLoader>();
        hud = FindObjectOfType<HUDController>();
        IsThisHardMode(LevelLoader.CurrentGameMode);
    }

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
