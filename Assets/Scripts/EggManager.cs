using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class EggManager : MonoBehaviour
{
    public int requiredEggs; //requiredEggs in NormalMode

    HUDController hud;
    LevelLoader levelLoader;

    void Awake()
    {
        hud = FindObjectOfType<HUDController>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    void Start()
    {
        SetRequiredEggs();
    }

    private void SetRequiredEggs()
    {
        if(levelLoader.CurrentGameMode == LevelLoader.GameMode.Normal)
        {
            hud.RequiredEggs = requiredEggs;
            hud.UpdateEggCountText();
        }
        
    }

}
