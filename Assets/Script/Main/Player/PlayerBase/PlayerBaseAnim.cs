using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseAnim : MonoBehaviour
{
    protected enum Anim
    {
        Idel,
        Run,
        Jump,
        Fall,
    };
    protected Anim state;

    protected Animator anim;
    protected PlayerBaseJump playerJump;
    protected PlayerBaseMove playerMove;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerBaseMove>();
        playerJump = GetComponent<PlayerBaseJump>();
    }

    // Update is called once per frame
    protected void Update()
    {
        // Debug.Log(playerMove);
        if (playerMove.MoveController != 0 && playerJump.isGround == true)
        {
            state = Anim.Run;
        }
        else if (playerJump.isGround == true)
        {
            state = Anim.Idel;
        }

        if (playerJump.rb.velocity.y > 0.3f)
        {
            state = Anim.Jump;
        }
        if (playerJump.rb.velocity.y < -0.3f && playerJump.isGround == false)
        {
            state = Anim.Fall;
        }

        anim.SetInteger("state", (int)state);
    }
}
