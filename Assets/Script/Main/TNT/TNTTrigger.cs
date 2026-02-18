using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Side
{
    Left,
    Right,
};

public class TNTTrigger : MonoBehaviour
{
    // [SerializeField] private GameObject TriggerManager;
    [SerializeField] private Side side;
    [SerializeField] private string emitButtonName1 = "Attack_1";
    [SerializeField] private string emitButtonName2 = "Attack_2";

    private Dropper dropper;


    private TNTTriggerManager manager;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        // manager = TriggerManager.GetComponent<TNTTriggerManager>();
        tr = GetComponent<Transform>();

        foreach(Transform child in transform.parent)
        {
            if (child != transform && child.name.Contains("Dropper"))
            {
                dropper = child.GetComponent<Dropper>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DoNotTriggerTheDropper"))
        {
            return;
        }
        // Debug.Log("TNT trigger triggered");

        SendMessageToManager();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player1") && Input.GetButtonDown(emitButtonName1))
        {
            SendMessageToManager();
        }
        if(other.CompareTag("Player2") && Input.GetButtonDown(emitButtonName2))
        {
            SendMessageToManager();
        }
    }

    private void SendMessageToManager()
    {
        //if (side == Side.Left)
        //{
        //    manager.OnLeftTriggered();
        //    tr.localScale = new Vector3(tr.localScale.x, -tr.localScale.y, tr.localScale.z);
        //}
        //else if (side == Side.Right)
        //{
        //    manager.OnRightTriggered();
        //    tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y, tr.localScale.z);
        //}
        dropper.Emit();
    }
}
