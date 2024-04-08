using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnToMainMenu : MonoBehaviour
{
    // The name of your main menu scene
    public string mainMenuSceneName = "Main Menu";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReturnToMenuAfterDelay(10)); // 10 seconds delay
    }

    IEnumerator ReturnToMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
