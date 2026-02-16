using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    winnerNameText;
    [SerializeField] private GameObject         Flag1;
    [SerializeField] private GameObject         Flag2;
    [SerializeField] private Canvas             canvas;

    private Animator anim1;
    private Animator anim2;
    private CanvasGroup canvasGroup;

    private int     player1Score    = 0;
    private int     player2Score    = 0;
    private string  winnerName      = "";

    // Start is called before the first frame update
    void Start()
    {
        player1Score = DataBridge.Instance.Player1Score;
        player2Score = DataBridge.Instance.Player2Score;

        anim1 = Flag1.GetComponent<Animator>();
        anim2 = Flag2.GetComponent<Animator>();
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        
        if (player1Score > player2Score)
        {
            winnerName = "Player 1";
        }
        else if (player2Score > player1Score)
        {
            winnerName = "Player 2";
        }
        else
        {
            winnerName = "cake is a lie";
        }

        winnerNameText.text = winnerName;
        Invoke(nameof(SetTrigger), 1f);
        Invoke(nameof(ShowUI), 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTrigger()
    {
        if(player1Score > player2Score)
        {
            anim1.SetTrigger("Win");
        }
        else if(player2Score > player1Score)
        {
            anim2.SetTrigger("Win");
        }
        else
        {
            anim1.SetTrigger("Win");
            anim2.SetTrigger("Win");
        }
    }
    private void ShowUI()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}
