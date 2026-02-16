using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private StartUpManager manager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RoundAdd()
    {
        manager.RoundRight();
    }
    public void RoundMinus()
    {
        manager.RoundLeft();
    }
    public void TimeAdd()
    {
        manager.TimeRight();
    }
    public void TimeMinus()
    {
        manager.TimeLeft();
    }
}
