using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDefaultLevel()
    {
        DataBridge.Instance.SetWorldLevel("Main");
        SceneManager.LoadScene("Main");
    }
    public void LoadCannonWorld()
    {
        DataBridge.Instance.SetWorldLevel("CannonWorld");
        SceneManager.LoadScene("CannonWorld");
    }
    public void Back()
    {
        SceneManager.LoadScene("StartUp");
    }
}
