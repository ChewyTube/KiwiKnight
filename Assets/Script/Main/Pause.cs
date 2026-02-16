using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    private CanvasGroup canvasGroup;

    void Start()
    {
        // pauseMenuUI.SetActive(false);

        canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
    }

    private void GamePause()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        isPaused = true;
        // pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameContinue()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        Time.timeScale = 1;
        isPaused = false;
        // pauseMenuUI.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        isPaused = false;
        Time.timeScale = 1;
        // pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
