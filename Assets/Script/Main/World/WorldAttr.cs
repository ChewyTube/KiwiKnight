using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldAttr : MonoBehaviour
{
    [SerializeField] private int                timePerRound;
    [SerializeField] private int                totalRound;
    [SerializeField] private TextMeshProUGUI    timeText;
    [SerializeField] private TextMeshProUGUI    roundText;

    private int currentTime;
    private int currentRound;
    private int minutes;
    private int seconds;

    private Player1Attr player1Attr;
    private Player2Attr player2Attr;
    private PlayerDead  player1Dead;
    private PlayerDead  player2Dead;

    private int player1Score;
    private int player2Score;

    private bool isShowingInfo;
    private float infoShowTime;

    private string currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        timePerRound = DataBridge.Instance.timePerRound;
        totalRound = DataBridge.Instance.totalRounds;

        currentTime = timePerRound;
        currentRound = 1;

        player1Attr = GameObject.Find("Player1").GetComponent<Player1Attr>();
        player2Attr = GameObject.Find("Player2").GetComponent<Player2Attr>();
        player1Dead = GameObject.Find("Player1").GetComponent<PlayerDead>();
        player2Dead = GameObject.Find("Player2").GetComponent<PlayerDead>();

        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTimer()
    {
        while (currentTime > 0 && currentRound <= totalRound)
        {
            currentTime -= 1;
            UpdateText();
            if (currentTime == 0)
            {
                UpdateGameState();
                ShowInfo();
                yield return new WaitForSeconds(2);
                RefreshPlayerState();
                roundText.color = Color.white; // reset color
            }
            //if (currentTime >= timePerRound - 3)
            //{
            //    ClearCloneSlots();
            //}

            yield return new WaitForSeconds(1);
        }


        DataBridge.Instance.Player1Score = player1Score;
        DataBridge.Instance.Player2Score = player2Score;

        // currentLevel = DataBridge.Instance.GetWorldLevel();
        // SceneManager.LoadScene(currentLevel);
        SceneManager.LoadScene("End");
    }

    void GetMinutesAndSeconds()
    {
        minutes = currentTime / 60;
        seconds = currentTime % 60;
    }
    void UpdateText()
    {
        GetMinutesAndSeconds();
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        roundText.text = "Round " + currentRound.ToString() + "/" + totalRound.ToString();
        if (currentTime <= 3)
        {
            timeText.color = Color.yellow;
        }
        else if (currentTime <= 10)
        {
            timeText.color = Color.green;
        }
        else
        {
            timeText.color = Color.white;
        }
    }
    void UpdateGameState()
    {
        if (player1Attr.deadCount > player2Attr.deadCount)
        {
            player2Score += 1;
        }
        else if (player2Attr.deadCount > player1Attr.deadCount)
        {
            player1Score += 1;
        }

        currentRound += 1;
        currentTime = timePerRound;

    }

    private static void ClearCloneSlots()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("CloneSlot"))
            {
                // Debug.Log(obj.tag);
                Destroy(obj);
            }
        }
    }

    private void RefreshPlayerState()
    {
        player1Attr.deadCount = 0;
        player2Attr.deadCount = 0;

        player1Dead.Rebirth();
        player2Dead.Rebirth();

        player1Attr.ChangeWeapon(new Weapon());
        player2Attr.ChangeWeapon(new Weapon());

        player1Attr.InitBullet();
        player2Attr.InitBullet();
    }

    private void ShowInfo()
    {
        if(player1Attr.deadCount > player2Attr.deadCount)
        {
            roundText.text = "Player 2 get a point";
        }
        else if (player2Attr.deadCount > player1Attr.deadCount)
        {
            roundText.text = "Player 1 get a point";
        }
        else
        {
            roundText.text = "It's a tie";
        }
        roundText.color = Color.blue;
    }
    public int GetCurrentTime()
    {
        return currentTime;
    }
    public int GetTimePerRound()
    {
        return timePerRound;
    }
    public void SetWorldLevel(string level)
    {
        currentLevel = level;
    }
}
