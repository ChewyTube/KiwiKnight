using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Anim : PlayerBaseAnim
{
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<Player2Move>();
        playerJump = GetComponent<Player2Jump>();
    }
}
