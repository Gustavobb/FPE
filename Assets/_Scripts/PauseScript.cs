using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pause;

    public AudioSource audioSource;
    void Start()
    {
        pause.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (GamePaused) Resume();
            else Pause();
        }   
    }

    public void Resume() {
        audioSource.Play(); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pause.SetActive(false);
        Time.timeScale = 1;
        GamePaused = false;

    }
     void Pause() {
        audioSource.Play(); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pause.SetActive(true);
        Time.timeScale = 0;
        GamePaused = true;
    }

    public void Exit() => Application.Quit();
}