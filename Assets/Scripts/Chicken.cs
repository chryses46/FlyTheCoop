//using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


namespace Game.Core
{

    public class Chicken : MonoBehaviour
    {
        [SerializeField] float rcsThrust = 175f;
        [SerializeField] float mainThrust = 20f;
        [SerializeField] float levelLoadDelay = 2f;

        [SerializeField] AudioClip startLevel;
        [SerializeField] AudioClip flyingChicken;
        [SerializeField] AudioClip death;
        [SerializeField] AudioClip win;

        [SerializeField] ParticleSystem flyingChickenParticles;
        [SerializeField] ParticleSystem deathParticles;
        [SerializeField] ParticleSystem winParticles;

        Rigidbody rigidBody;
        AudioSource audioSource;

        LevelLoader levelLoader;
        public bool isTransitioning = false;

        bool collisionsDisabled = false;

        RigidbodyConstraints originalConstraints;

        // Start is called before the first frame update
        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            levelLoader = FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>();
            originalConstraints = rigidBody.constraints;
        }
    
        void Update()
        {
            if (!isTransitioning)
            {
                RespondToRotateInput();
                RespondToThrustInput();
            }

            if (Debug.isDebugBuild) 
            { 
                RespondToDebugKeys();
            }

            PauseGame();
        }

        private void RespondToDebugKeys()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LevelLoader.NextScene();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                collisionsDisabled = !collisionsDisabled;
            }
        }

        void OnCollisionEnter(Collision collision)
        {

            if (isTransitioning || collisionsDisabled) { return; }

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    StartWinSequence();
                    break;
                default:
                    StartDeathSequence();
                    break;
            }
        }

        private void StartWinSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(win);
            winParticles.Play();
            
            StartCoroutine(levelLoader.LoadSceneWithDelay());
        }

        private void StartDeathSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathParticles.Play();

            // Load scene based on game mode
            StartCoroutine(levelLoader.LoadSceneWithDelay(levelLoader.CurrentGameMode, false));
        }

        private void RespondToRotateInput()
        {

            float rotationThisFrame = rcsThrust * Time.deltaTime;

            rigidBody.freezeRotation = true; // take manual control of rotation

            if (Input.GetKey(KeyCode.A)) // can only press A or B, but not at the same time
            {

                if (SceneManager.GetActiveScene().name == "Level5")
                {
                    transform.Rotate(-Vector3.forward * rotationThisFrame);
                }
                else
                {
                    transform.Rotate(Vector3.forward * rotationThisFrame);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (SceneManager.GetActiveScene().name == "Level5")
                {
                    transform.Rotate(Vector3.forward * rotationThisFrame);
                }
                else
                {
                    transform.Rotate(-Vector3.forward * rotationThisFrame);
                }

            }
        }

        void RespondToThrustInput()
        {
            if (Input.GetKey(KeyCode.Space))// Can thrust while rotating
            {
                ApplyThrust();
            }
            else
            {
                StopApplyingThrust();
            }

        }

        private void StopApplyingThrust()
        {
            audioSource.Stop();
            flyingChickenParticles.Stop();
        }

        private void ApplyThrust()
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            flyingChickenParticles.Play();

            if (!audioSource.isPlaying && !isTransitioning) // Audio will not layer
            {
                audioSource.PlayOneShot(flyingChicken);
            }

        }

        public void PauseGame()
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if(levelLoader.CurrentGameState == LevelLoader.GameState.Play)
                {
                    levelLoader.CurrentGameState = LevelLoader.GameState.Pause;
                    levelLoader.PauseScreenControl();
                    isTransitioning = true;
                    rigidBody.constraints = RigidbodyConstraints.FreezeAll;

                }else{

                    levelLoader.CurrentGameState = LevelLoader.GameState.Play;
                    levelLoader.PauseScreenControl();
                    isTransitioning = false;
                    rigidBody.constraints = originalConstraints;
                }
            }
        }
    }
}