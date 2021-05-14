
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LeveloadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;

    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void update()
    {
        RespondToDebugKeys();

    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    void OnCollisionEnter(Collision other)
    {


        if (isTransitioning || collisionDisabled ) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }

    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LeveloadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(currentSceneIndex +1);
        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        SceneManager.LoadScene(NextSceneIndex);
    }
        
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
