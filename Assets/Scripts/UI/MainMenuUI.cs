using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlyTheCoop.Core;

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
        [SerializeField] Button versionInfoCloseButton;
        public bool shown = false;
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
            versionInfoCloseButton.onClick.AddListener(HideVersionInfo);
        }
        private void EggStore()
        {
            if(versionInfo.activeSelf == true)
            {
                HideVersionInfo();
            }

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
            if(versionInfo.activeSelf == true)
            {
                HideVersionInfo();
            }
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
        private void ShowVersionInfo()
        {
            if(shown)
            {
                HideVersionInfo();
                shown = false;
            }
            else
            {
                if(ui.noticeWindow.activeSelf == true)
                {
                    ui.CloseNoticeWindow();
                }
                versionInfo.SetActive(true);
                shown = true; 
            }
        }
        public void HideVersionInfo()
        {
            versionInfo.SetActive(false);
            shown = false;
        }
        public void GetVersionNumber()
        {
            string versionNumber = "v." + Application.version;
            versionText.text = versionNumber;
        }
    }
}
