using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Text normalModeText;
    [SerializeField] Text hardModeText;

    LevelLoader levelLoader;
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();


        if(levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard)
        {
            EnableHardModeText();
        }
        else
        {
            EnableNormalModeText();
        }

    }

    private void EnableNormalModeText()
    {
        normalModeText.gameObject.SetActive(true);
    }

    private void EnableHardModeText()
    {
        hardModeText.gameObject.SetActive(true);
    }
}
