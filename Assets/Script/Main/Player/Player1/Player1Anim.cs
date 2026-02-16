using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Anim : PlayerBaseAnim
{
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<Player1Move>();
        playerJump = GetComponent<Player1Jump>();
    }
}
