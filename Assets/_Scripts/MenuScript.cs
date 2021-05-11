using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject loadingScreen;
    public Slider slider;

    void Start() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Play() {
        audioSource.Play(); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManagerScript.nextEventTrigger = null;
        GameManagerScript.eventChanged = null;
        StartCoroutine(LoadAsynchrously());
    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }
    public void Quit() {
        Application.Quit();
    }

    IEnumerator LoadAsynchrously()
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress  = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}