using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void GameRestart()
    {
        SceneManager.LoadScene(3);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
