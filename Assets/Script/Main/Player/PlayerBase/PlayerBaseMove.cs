using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseMove : MonoBehaviour
{
    protected Rigidbody2D       rb; 
    protected Animator          anim;
    protected Transform         tr;
    protected PlayerBaseJump    jump;
    public    float             moveSpeed;
    [SerializeField] protected float    dashSpeed;
    [SerializeField] protected string   buttonName;
    [SerializeField] protected KeyCode  dashKey;
    public float    MoveController;
    protected bool  isRun;
    protected bool  canDash     = true ;
    protected bool  isDashing   = false;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        jump = GetComponent<PlayerBaseJump>();
    }

    // Update is called once per frame
    protected void Update()
    {
        bool isGround = anim.GetBool("isGround");
        if (!jump.isWallJumping && !isDashing)
        {
            XMove(isGround);
        }
        CheckIsRun();
        FlipPlayer();

        SendVars();
    }

    protected void FlipPlayer()
    {
        if (rb.velocity.x > 0)
        {
            tr.localScale = new Vector2(1, tr.localScale.y);
        }
        if (rb.velocity.x < 0)
        {
            tr.localScale = new Vector2(-1, tr.localScale.y);
        }
    }

    protected void SendVars()
    {
        anim.SetBool("isRun", isRun);
    }

    protected void CheckIsRun()
    {
        isRun = MoveController != 0;
    }

    protected virtual void XMove(bool isGround)
    {
        MoveController = Input.GetAxisRaw(buttonName);
        // Debug.Log(MoveController);

        if (isGround || (!isGround && MoveController != 0))
        {
            rb.velocity = new Vector2(MoveController * moveSpeed, rb.velocity.y);
        }
        if(Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    protected IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity += new Vector2(
            rb.velocity.x * 0.4f + dashSpeed * tr.localScale.x, 0
            );
        canDash = false;
        yield return new WaitForSeconds(0.1f);
        canDash = true;
        rb.velocity -= new Vector2(
            dashSpeed * tr.localScale.x, 0
            );
        isDashing = false;
    }
    
    protected virtual void SetDashKey(KeyCode key)
    {
        dashKey = key;
    }
}
