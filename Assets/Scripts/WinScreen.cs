using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Text normalModeText;
    [SerializeField] Text hardModeText;
    [SerializeField] Text eggsCollected;
    [SerializeField] Text mostEggsCollected;
    [SerializeField] Button newGame;

    LevelLoader levelLoader;
    HUDController hud;
    PlayerPrefConfig ppc;
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        hud = FindObjectOfType<HUDController>();
        ppc = FindObjectOfType<PlayerPrefConfig>();
        DetermineText();
        SetEggsCollectedText();
        SetMostEggsText();
        levelLoader.HUDEnabled(false);
        newGame.onClick.AddListener(CallNewGame);

    }

    private void DetermineText()
    {
        if (levelLoader.CurrentGameMode == LevelLoader.GameMode.Hard)
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

    private void SetEggsCollectedText()
    {
        //Debug.Log("TotalScore: " + (hud.TotalScore).ToString();
        eggsCollected.text = "Eggs Collected: " + hud.TotalScore;
    }

    private void SetMostEggsText()
    {
        mostEggsCollected.text = "Most Eggs Collected: " + ppc.HighScore;
    }

    private void CallNewGame()
    {
        levelLoader.LoadModeScene();
    }
}
