using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBridge : MonoBehaviour
{
    public int Player1Score { get; set; } = 0;
    public int Player2Score { get; set; } = 0;

    public int totalRounds = 3;
    public int timePerRound = 180;

    private string currentLevel = "Main";

    private static DataBridge _instance;
    public static DataBridge Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<DataBridge>();
                DontDestroyOnLoad(obj); // 跨场景保留
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // 避免重复创建
        }
    }
    public void SetWorldLevel(string level)
    {
        currentLevel = level;
    }
    public string GetWorldLevel()
    {
        return currentLevel;
    }

}
