﻿using FlyTheCoop.Core;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FlyTheCoop.Player
{
    public class Chicken : MonoBehaviour
    {
#region PublicProperties
        [Header("Physics Properties")]
        [SerializeField] float rcsThrust = 175f;
        [SerializeField] float mainThrust = 20f;

        [Header("Sound Effects")]
        [SerializeField] AudioClip flyingChicken;
        [SerializeField] AudioClip death;
        [SerializeField] AudioClip win;

        [Header("Particle Effects")]
        [SerializeField] ParticleSystem flyingChickenParticles;
        [SerializeField] ParticleSystem deathParticles;

        [Header("Debug Bools")]
        public bool isTransitioning = false;
        public bool collisionsDisabled = false;
#endregion
#region PrivateFields
        private RigidbodyConstraints originalConstraints;
#endregion
#region TypeReferences
        Rigidbody rigidBody;
        AudioSource audioSource;
        LevelLoader levelLoader;
#endregion
#region Startup
        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            levelLoader = FindObjectOfType<LevelLoader>();
            originalConstraints = rigidBody.constraints;
        }
        void Update()
        {
            if (!isTransitioning)
            {
                RespondToRotateInput();
                RespondToThrustInput();
            }

            PauseGame();
        }
#endregion        
#region TransitionMethods
        private void StartWinSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(win);
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
#endregion
#region ControlMethods
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
#endregion
    void OnCollisionEnter(Collision collision)
        {

            if (isTransitioning || collisionsDisabled) { return; }

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "PickUp":
                    break;
                case "Finish":
                    StartWinSequence();
                    break;
                default:
                    StartDeathSequence();
                    break;
            }
        }
    }
}