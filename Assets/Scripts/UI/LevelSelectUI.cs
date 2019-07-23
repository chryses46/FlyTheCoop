using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;

namespace FlyTheCoop.UI
{
    public class LevelSelectUI : MonoBehaviour
    {
    [SerializeField] Button backButton;
    [Header("Drop level button in this list.")]
    [Tooltip("Button must have a child GameObject with a Text component with the exact name of the scene as found in Build Settings.")]
    public List<Button> levelButtons = new List<Button>();

    LevelLoader levelLoader;

    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        SetListeners();
    }

    private void SetListeners()
    {
        for(int i = 0; i < levelButtons.Count; i++)
        {
            
            //Get text of the button
            Text buttonText = levelButtons[i].transform.GetComponentInChildren<Text>();
            string buttonName = buttonText.text;

            levelButtons[i].onClick.AddListener(delegate{levelLoader.SelectLevel(buttonName);});
        }

        backButton.onClick.AddListener(levelLoader.LoadMainMenu);
    }
    }
}
