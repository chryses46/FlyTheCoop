using FlyTheCoop.Core;
using FlyTheCoop.UI;
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
#region TypeReferences
        Rigidbody rigidBody;
        AudioSource audioSource;
        LevelLoader levelLoader;
        UIController ui;
        StateController state;
#endregion
#region Startup
        public void Start()
        {
            Time.timeScale = 1;
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            levelLoader = FindObjectOfType<LevelLoader>();
            ui = FindObjectOfType<UIController>();
            state = FindObjectOfType<StateController>();
/*  Currently pausing mobile development until further notice.
            TouchManager testingTouch = FindObjectOfType<TouchManager>(); 
            testingTouch.chicken = this;
*/
        }
        void Update()
        {
            if (!isTransitioning)
            {
                RespondToRotateInput();
                RespondToThrustInput();
            }
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
            flyingChickenParticles.Stop();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            // Load scene based on game mode
            StartCoroutine(levelLoader.LoadSceneWithDelay(state.CurrentGameMode, false));
        }
#endregion
#region ControlMethods
        public void RespondToRotateInput(int rotateKey = 0)
        {
            float rotationThisFrame = rcsThrust * Time.deltaTime;

            rigidBody.freezeRotation = true; // take manual control of rotation

            if (Input.GetKey(KeyCode.A) || rotateKey == 1) // can only press A or B, but not at the same time
            {
                if (SceneManager.GetActiveScene().name == "Garage")
                {
                    transform.Rotate(-Vector3.forward * rotationThisFrame);
                }
                else
                {
                    transform.Rotate(Vector3.forward * rotationThisFrame);
                }
            }
            else if (Input.GetKey(KeyCode.D) || rotateKey == 2)
            {
                if (SceneManager.GetActiveScene().name == "Garage")
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
        public void StopApplyingThrust()
        {
            audioSource.Stop();
            flyingChickenParticles.Stop();
        }
        public void ApplyThrust()
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            flyingChickenParticles.Play();

            if (!audioSource.isPlaying && !isTransitioning) // Audio will not layer
            {
                audioSource.PlayOneShot(flyingChicken);
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