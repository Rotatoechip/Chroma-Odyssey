using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance = null;
    private AudioSource audioSource;

    public AudioClip mainMenuAndEndSceneMusic; // Assign in Inspector
    public AudioClip level1Music; // Assign in Inspector
    public AudioClip level2Music; // Assign in Inspector

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Main Menu":
            case "End scene": // Use same music for "Main Menu" and "End scene"
                PlayMusic(mainMenuAndEndSceneMusic);
                break;
            case "Level 1":
                PlayMusic(level1Music);
                break;
            case "Level 2":
                PlayMusic(level2Music);
                break;
                // No default case needed unless you want to handle a specific case
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
