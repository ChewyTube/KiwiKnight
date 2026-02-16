using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTTriggerManager : MonoBehaviour
{
    // [SerializeField] private GameObject manager;
    // [SerializeField] private GameObject dropperLeft;
    // [SerializeField] private GameObject dropperRight;
    // [SerializeField] private GameObject triggerLeft;
    // [SerializeField] private GameObject triggerRight;

    private bool isTriggeredLeft = false;
    private bool isTriggeredRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeftTriggered()
    {
        isTriggeredLeft = true;
    }
    public void OnRightTriggered()
    {
        isTriggeredRight = true;
    }
    public bool IsLeftTriggered()
    {
        return isTriggeredLeft;
    }   
    public bool IsRightTriggered()
    {
        return isTriggeredRight;
    }
    public void ResetTriggerLeft()
    {
        isTriggeredLeft = false;
    }
    public void ResetTriggerRight()
    {
        isTriggeredRight = false;
    }
}
