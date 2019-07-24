using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;
using System;

namespace FlyTheCoop.UI
{
    public class MainMenuUI : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Button newGame;
        [SerializeField] Button controls;
        [SerializeField] Button levelSelect;
        [SerializeField] Button achievements;
        [SerializeField] Button versionButton;
        [SerializeField] GameObject versionInfo;
        [SerializeField] Text versionText;
        public bool shown;
#endregion
#region TypeReferences
        UIController ui;
        LevelLoader levelLoader;
        PlayerPrefConfig ppc;
#endregion
#region StartUp
        void Awake()
        {
            ui = FindObjectOfType<UIController>();
            levelLoader = FindObjectOfType<LevelLoader>();
            ppc = FindObjectOfType<PlayerPrefConfig>();
            AddListeners();
        }
        void Start()
        {
            GetVersionNumber();
        }
        void Update()
        {
            HideVersionInfoOnClickAway();
        }
#endregion
        private void AddListeners()
        {
            newGame.onClick.AddListener(levelLoader.LoadModeScene);
            controls.onClick.AddListener(ui.DisplayControlScreen);
            levelSelect.onClick.AddListener(CheckGameCompleteStatus);
            versionButton.onClick.AddListener(ShowVersionInfo);
            achievements.onClick.AddListener(LoadAchievements);
        }

        private void CheckGameCompleteStatus()
        {
            if(ppc.IsNormalModeCompleted())
            {
                levelLoader.LoadLevelSelect();
            }
            else
            {
                ui.CallNoticeWindow("Level Select is unlocked once you complete the game in Normal Mode!");
            }
        }

        private void LoadAchievements()
        {
            ui.CallNoticeWindow("Achievements coming soon! Please check back later.");
        }
        void HideVersionInfoOnClickAway()
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
            {
                if(shown)
                {
                    HideVersionInfo();
                }
            }
        }
        private void ShowVersionInfo()
        {
            if(shown == true)
            {
                shown = false;
            }

            float yStart = versionInfo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 1000)
            {
                float yEnd = 0;
                StartCoroutine(MoveObject(versionInfo, yStart, yEnd));
                
            }

            shown = true;
        }
        private void HideVersionInfo()
        {
            float yStart = versionInfo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 0)
            {
                float yEnd = 1000;
                StartCoroutine(MoveObject(versionInfo, yStart, yEnd));
                
            }

            shown = false;
        }
        IEnumerator MoveObject(GameObject go, float start, float end)
        {
            RectTransform goRect = go.GetComponent<RectTransform>();
            float t = 0.0f;

            while(t < 1)
            {
                t += Time.deltaTime / .5f;
                goRect.localPosition = new Vector2(0,Mathf.Lerp(start,end,t));
                yield return null;
            }
        }
        public void GetVersionNumber()
        {
            string versionNumber = "v." + Application.version;
            versionText.text = versionNumber;
        }
    }
}
