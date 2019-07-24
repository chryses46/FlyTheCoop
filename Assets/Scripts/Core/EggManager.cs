using UnityEngine;
using FlyTheCoop.UI;

namespace FlyTheCoop.Core
{
    public class EggManager : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] HUDController hUD;
        [SerializeField] AudioClip collectEgg;
        [Tooltip("For HardMode: A higher number means more eggs required to advance in level.")]
        public int requiredModifier = 3;
        public GameObject Exit;
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
        private const int initialRequired = 3; // Base for TotalEgg formula around
        private int _requiredEggs;
        public int LevelScore { get; private set; }
        public int TotalScore { get; private set; }
        public int HardModeEggsThisLevel {get; private set;}
#endregion
#region TypeReferences
        
        LevelLoader levelLoader;
        StateController state;
        PlayerPrefConfig ppc;
        AudioSource audioSource;
#endregion
#region Startup
        void Awake()
        {
            levelLoader = GetComponent<LevelLoader>();
            state = GetComponent<StateController>();
            ppc = GetComponent<PlayerPrefConfig>();
            audioSource = GetComponent<AudioSource>();
        }
#endregion
#region ScoreManagement
        public void EggCollected()
        {
            audioSource.PlayOneShot(collectEgg);
            
            if(state.CurrentGameMode == StateController.GameMode.Hard)
            {
                TotalScore += 1;
                HardModeEggsThisLevel += 1;
            }
            else
            {
                LevelScore += 1;
            }
            
            UpdateExit();
            hUD.UpdateEggCountText();
        }
        public void UpdateTotalScore() // called on both Hard and Normal Mode
        {
            TotalScore += LevelScore;
            if(state.CurrentGameMode == StateController.GameMode.Hard)
            {
                ppc.ManageEggBasket(HardModeEggsThisLevel);
                HardModeEggsThisLevel = 0;
            }
            else
            {
               ppc.ManageEggBasket(LevelScore); 
            }
            
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
            hUD.UpdateEggCountText();
        }
        public void ResetLevelScore()
        {
            LevelScore = 0;
            hUD.UpdateEggCountText();
            
        }
        public void SetRequiredEggs(int requiredEggs)
        {
            if(state.CurrentGameMode == StateController.GameMode.Normal)
            {
                RequiredEggs = requiredEggs;
                hUD.UpdateEggCountText();
            }
        } 
        public void UpdateRequiredHardModeEggsCount(int currentLevelIndex)
        {
            RequiredEggs = initialRequired + currentLevelIndex * requiredModifier;
            hUD.UpdateEggCountText();
        }
#endregion
#region LevelProgressionManagement
        public void UpdateExit(GameObject exit, int requiredEggs)
        { 
            Exit = exit;
            SetRequiredEggs(requiredEggs);
            Exit.tag = "Untagged"; // Disables the Exit

            UpdateExit();
        }
        public void UpdateExit()
        {
            if(state.CurrentGameMode == StateController.GameMode.Hard && TotalScore >= _requiredEggs)
            {
                EnableExit();
            }
            else if(LevelScore >= _requiredEggs)
            {
                EnableExit();
            }
        }

        private void EnableExit()
        {
            Exit.tag = "Finish";
            if(Exit.transform.Find("Light"))
            {
                Exit.transform.Find("Light").gameObject.SetActive(true);
            }
            Exit.transform.Find("Effect").GetComponent<ParticleSystem>().Play();

            Debug.Log("Exit unlocked: " + Exit.tag);
        }
        #endregion
    }
}
