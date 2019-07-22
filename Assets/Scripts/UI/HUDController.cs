using FlyTheCoop.Core;
using FlyTheCoop.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class HUDController : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Text eggCountText;
        [SerializeField] AudioClip collectEgg;
        [SerializeField] int requiredModifier = 3; //for HardMode: a higher number means more eggs required to move forward
        public GameObject FinishLine;
        public int RequiredEggs 
        { 
            get 
            {
                return _requiredEggs;
            } 
            set
            {
                _requiredEggs = value;
            } 
        }
#endregion
#region PrivateFields
        protected const int initialRequired = 3; // const to base TotalEgg formula around
        private int _requiredEggs;
        private int LevelScore { get; set; }
        public int TotalScore { get; private set; }
#endregion
#region TypeReferences
        PlayerPrefConfig ppc;
        AudioSource audioSource;
        LevelLoader levelLoader;
        StateController state;
#endregion
#region Startup
        void OnEnable()
        {
            ppc = FindObjectOfType<PlayerPrefConfig>();
            audioSource = GetComponent<AudioSource>();
            levelLoader = FindObjectOfType<LevelLoader>();
            state = FindObjectOfType<StateController>();

        }
#endregion
#region ScoreManagement
        public void EggCollected()
        {
            audioSource.PlayOneShot(collectEgg);
            
            if(state.CurrentGameMode == StateController.GameMode.Hard)
            {
                TotalScore += 1;
            }
            else
            {
                LevelScore += 1;
            }
            
            UpdateFinishLine();
            UpdateEggCountText();
        }
        public void UpdateTotalScore()
        {
            TotalScore += LevelScore;
            ResetLevelScore();
        }
        public void UpdateHighScore()
        {
            if(TotalScore > ppc.HighScore)
            {
                ppc.ManageHighScore(TotalScore);
            }
        }
        public void ResetScore()
        {
            TotalScore = 0;

            if(state.CurrentGameMode == StateController.GameMode.Hard)
            {
                UpdateRequiredHardModeEggsCount(0);
            } 
            ResetLevelScore();
            UpdateEggCountText();
        }
        public void ResetLevelScore()
        {
            LevelScore = 0;
            UpdateEggCountText();
            
        }
#endregion
#region LevelProgressionManagement
        public void UpdateFinishLine(GameObject finish)
        { 
            FinishLine = finish;

            FinishLine.tag = "Untagged"; // Disables the FinishLine
            Debug.Log("FinishLine locked: " + FinishLine.tag);
        }

        public void UpdateFinishLine()
        {
            if(state.CurrentGameMode == StateController.GameMode.Hard && TotalScore >= _requiredEggs)
            {
                FinishLine.tag = "Finish";
                Debug.Log("FinishLine unlocked: " + FinishLine.tag);
            }
            else if(LevelScore >= _requiredEggs)
            {
                FinishLine.tag = "Finish";
                Debug.Log("FinishLine unlocked: " + FinishLine.tag);
            }
        }
#endregion
#region UIControl
        public void UpdateEggCountText()
        {   
            if(state.CurrentGameMode == StateController.GameMode.Hard)
            {
                eggCountText.text = (LevelScore + TotalScore) + "/" + _requiredEggs;
            }
            else
            {
                eggCountText.text = LevelScore + "/" + _requiredEggs;
            }
        }
        public void UpdateRequiredHardModeEggsCount(int currentLevelIndex)
        {
            if(currentLevelIndex >= 0)
            {
                RequiredEggs = initialRequired + currentLevelIndex * requiredModifier;
                UpdateEggCountText();
            }
        }
#endregion
    }
}