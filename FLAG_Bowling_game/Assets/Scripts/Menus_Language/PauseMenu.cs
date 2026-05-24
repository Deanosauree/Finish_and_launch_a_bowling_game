using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    PlayerInputs playerInputs;
    InputAction menu;
    bool isPaused = false;
    public Animator transition;
    public float transitionTime = 2f;
    void Awake()
    {
        playerInputs = new PlayerInputs();
    }
    private void OnEnable()
    {
        playerInputs.Enable();
        menu = playerInputs.Menu.Escape;
        menu.Enable();
        menu.performed += Pause;
    }
    private void OnDisable()
    {
        playerInputs.Disable();
        menu.Disable();
    }
    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            ActivatePause();
        }
        else
        {
            DeactivatePause();
        }
    }
    public void ActivatePause()
    {
        AudioListener.pause = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void DeactivatePause()
    {
        AudioListener.pause = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Continue()
    {
        isPaused = !isPaused;
        DeactivatePause();
    }
    public void BackToMain()
    {
        Continue();
        StartCoroutine(LoadLevel(0));
        //SceneManager.LoadScene(0);
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
