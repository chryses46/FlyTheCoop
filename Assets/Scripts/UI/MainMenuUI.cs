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
        [SerializeField] Button newGameButton;
        [SerializeField] Button controlsButton;
        [SerializeField] Button levelSelectButton;
        [SerializeField] Button achievementsButton;
        [SerializeField] Button eggStoreButton;
        [SerializeField] Text eggBasketCount;
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
        void Start()
        {
            ui = FindObjectOfType<UIController>();
            levelLoader = FindObjectOfType<LevelLoader>();
            ppc = FindObjectOfType<PlayerPrefConfig>();
            AddListeners();
            SetEggBasketCountText();
            CheckLevelSelectEnabled();
            GetVersionNumber();
        }
        void Update()
        {
            HideVersionInfoOnClickAway();
        }
#endregion
        private void AddListeners()
        {
            newGameButton.onClick.AddListener(levelLoader.LoadModeScene);
            controlsButton.onClick.AddListener(ui.DisplayControlScreen);
            levelSelectButton.onClick.AddListener(CheckGameCompleteStatus);
            versionButton.onClick.AddListener(ShowVersionInfo);
            achievementsButton.onClick.AddListener(LoadAchievements);
            eggStoreButton.onClick.AddListener(EggStore);

        }
        private void EggStore()
        {
            ui.CallNoticeWindow("Check back soon for cool add-ons and unlocks you can buy with eggs from your Egg Basket!");
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
        private void SetEggBasketCountText()
        {
            eggBasketCount.text = ppc.EggBasket.ToString();
        }
        private void CheckLevelSelectEnabled()
        {
            if(ppc.IsNormalModeCompleted())
            {
                Image levelSelectButtonImage = levelSelectButton.GetComponent<Image>();
                Color levelSelectButtonColor = levelSelectButtonImage.color;
                levelSelectButtonColor.a = 100;
                levelSelectButtonImage.color = levelSelectButtonColor;

                Text levelSelectText = levelSelectButton.transform.GetComponentInChildren<Text>();
                Color levelSelectTextColor = levelSelectText.color;
                levelSelectTextColor.a = 100;
                levelSelectText.color = levelSelectTextColor;
            }
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
