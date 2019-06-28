using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class FinishLine : MonoBehaviour
{
    HUDController hud;

    void OnEnable()
    {
        hud = FindObjectOfType<HUDController>();
        
    }

    void Start()
    {
        hud.UpdateFinishLine(gameObject);
    }
}
