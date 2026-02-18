using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void GameRestart()
    {
        SceneManager.LoadScene("StartUp");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
