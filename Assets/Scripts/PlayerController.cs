using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {

//This class attaches to the Player (Chicken).
        [SerializeField] float rotationThrust = 175f;
        [SerializeField] float mainThrust = 20f;
        
        
        DebugAgent debugAgent;
        EffectsFactory effectsFactory;
        LevelLoader levelLoader;
        Rigidbody rigidBody;

        public void Start()
        {
            //Core Components
            debugAgent = FindObjectOfType<DebugAgent>();
            effectsFactory = FindObjectOfType<EffectsFactory>();
            levelLoader = FindObjectOfType<LevelLoader>();
            //GameObject Components
            rigidBody = GetComponent<Rigidbody>();

        }

        void Update()
        {
            if (!effectsFactory.isTransitioning)
            {
                RespondToRotateInput(rotationThrust);
                RespondToThrustInput();
            }

            if (Debug.isDebugBuild) 
            { 
                debugAgent.RespondToDebugKeys(); //Core
            }
        }

         void OnCollisionEnter(Collision collision)
        {

            if (effectsFactory.isTransitioning || debugAgent.collisionsDisabled) { return; }

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Player":
                    break;
                case "Finish":
                    StartWinSequence();
                    break;
                default:
                    StartDeathSequence();
                    break;
            }
        }

        public void RespondToRotateInput(float thrust) //Control
        {

            float rotationThisFrame = thrust * Time.deltaTime;
            rigidBody.freezeRotation = true; // take manual control of rotation

            if (Input.GetKey(KeyCode.A)) // can only press A or B, but not at the same time
            {

                if (SceneManager.GetActiveScene().buildIndex == 4)
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
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    transform.Rotate(Vector3.forward * rotationThisFrame);
                }
                else
                {
                    transform.Rotate(-Vector3.forward * rotationThisFrame);
                }

            }
        }

        public void RespondToThrustInput() //Control
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

        private void StopApplyingThrust() // Control
        {
            effectsFactory.PlayerFlying(false);
        }

        private void ApplyThrust()//control
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            effectsFactory.PlayerFlying(true);
        }

         public void StartWinSequence() 
        {
            effectsFactory.PlayerWins();
            StartCoroutine(levelLoader.LoadNextScene());
        }

        public void StartDeathSequence()
        {
            effectsFactory.PlayerDies();
            //levelLoader.LoadFirstScene();
        }
    }
}
