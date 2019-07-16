using UnityEngine;

namespace FlyTheCoop.Core
{
    public class EffectsFactory: MonoBehaviour
    {
// This class is a component of GameObject "System"
        
        [SerializeField] AudioClip death;
        [SerializeField] AudioClip win;
        [SerializeField] AudioClip flyingAudio;
        [SerializeField] AudioClip startLevel;
        [SerializeField] ParticleSystem flyingParticles;
        [SerializeField] ParticleSystem deathParticles;
        [SerializeField] ParticleSystem winParticles;
        GameObject player;
        AudioSource audioSource;

        public bool isTransitioning = false;

        public bool isPlayerThrusting = false;
        void Start()
        {
            //GameObject Components
            audioSource = GetComponent<AudioSource>();

            player = GameObject.FindWithTag("Player");
            Debug.Log(player.transform.position);
        }
        public void PlayerWins()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(win);
            Instantiate (winParticles, player.transform.position, player.transform.rotation);
        }

        public void PlayerDies()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            Instantiate (deathParticles, player.transform.position, player.transform.rotation);
        }

        public void PlayerFlying(bool flying)
        {
            if(flying)
            {
                flyingParticles.Play();

                if (!audioSource.isPlaying && !isTransitioning) // Audio will not layer
                {
                    audioSource.PlayOneShot(flyingAudio);
                }
            }
            flyingParticles.Stop();
        }

    }
}