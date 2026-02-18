using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpManager : MonoBehaviour
{
    List<int> totalRounds = 
        new List<int>()
        { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19};
    List<int> timePerRound = 
        new List<int>()
        { 10, 20, 30, 60, 90, 120, 150, 180, 240, 300, 360, 420, 480, 540, 600};
    private int currentRoundIndex = 1;
    private int currentTimeIndex = 7;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timeText;

    //private static StartUpManager _instance = new StartUpManager();
    //public static StartUpManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            GameObject obj = new GameObject("GameManager");
    //            _instance = obj.AddComponent<StartUpManager>();
    //            // DontDestroyOnLoad(obj); // 跨场景保留
    //        }
    //        return _instance;
    //    }
    //}
    //private void Awake()
    //{
    //    if (_instance != null && _instance != this)
    //    {
    //        Destroy(gameObject); // 避免重复创建
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(roundText);
        // Debug.Log(totalRounds[currentRoundIndex].ToString());

        roundText.text = totalRounds[currentRoundIndex].ToString();
        timeText.text = timePerRound[currentTimeIndex].ToString();
    }

    public void RoundRight()
    {
        currentRoundIndex++;
        if (currentRoundIndex >= totalRounds.Count)
        {
            currentRoundIndex = totalRounds.Count - 1;
        }
    }
    public void RoundLeft()
    {
        currentRoundIndex--;
        if (currentRoundIndex < 0)
        {
            currentRoundIndex = 0;
        }
    }
    public void TimeRight()
    {
        currentTimeIndex++;
        if (currentTimeIndex >= timePerRound.Count)
        {
            currentTimeIndex = timePerRound.Count - 1;
        }
    }
    public void TimeLeft()
    {
        currentTimeIndex--;
        if (currentTimeIndex < 0)
        {
            currentTimeIndex = 0;
        }
    }
    public void StartGame()
    {
        DataBridge.Instance.totalRounds = totalRounds[currentRoundIndex];
        DataBridge.Instance.timePerRound = timePerRound[currentTimeIndex];
        SceneManager.LoadScene("ChooseLevel");
    }
}
