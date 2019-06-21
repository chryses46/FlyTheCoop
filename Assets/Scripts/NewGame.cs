using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;

public class NewGame : MonoBehaviour
{
    [SerializeField] Button newGame;

    LevelLoader levelLoader;
    void OnEnable()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        newGame.onClick.AddListener(levelLoader.LoadModeScene);
    }
}
