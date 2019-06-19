//using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

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

    bool isTransitioning = false;

    bool collisionsDisabled = false;

    // Start is called before the first frame update
    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
        
      
    // Update is called once per frame
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
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
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
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstScene", levelLoadDelay);
    }

    private void LoadFirstScene()
    {
        //audioSource.PlayOneShot(startLevel);
        SceneManager.LoadScene(2);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex;

        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            //audioSource.PlayOneShot(startLevel);
            SceneManager.LoadScene(2);
        }
        else 
        {
            nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex); 
            //audioSource.PlayOneShot(startLevel); 
        }
    }

    private void RespondToRotateInput()
    {

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        rigidBody.freezeRotation = true; // take manual control of rotation

        if (Input.GetKey(KeyCode.A)) // can only press A or B, but not at the same time
        {

            if (SceneManager.GetActiveScene().buildIndex == 6)
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
            if (SceneManager.GetActiveScene().buildIndex == 6)
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
}
