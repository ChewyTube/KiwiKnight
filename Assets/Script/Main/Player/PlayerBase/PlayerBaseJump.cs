using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseJump : MonoBehaviour
{
    public Rigidbody2D rb;
    protected Animator anim;
    protected Transform tr;
    protected WallCheck wc;
    [SerializeField] protected float jumpSpeed;
    [SerializeField] protected float wallJumpSpeed;
    [SerializeField] protected LayerMask GroundLayer;
    [SerializeField] protected float fallExtraAcceleration;
    [SerializeField] protected float extraGravity;
    [SerializeField] protected string ButtonName;
    protected float JumpController;
    protected bool canDoubleJump;
    public bool isWallJumping;
    public bool isJump;
    public bool isGround;


    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        wc = GetComponentsInChildren<WallCheck>()[0];
    }

    // Update is called once per frame
    protected void Update()
    {
        CheckOnGround();
        YMove();

        CheckIsJump();
        wallJump();
        SendVars();
    }

    protected virtual void YMove()
    {
        if (Input.GetButtonDown(ButtonName) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            canDoubleJump = true;
        }
        if (Input.GetButtonDown(ButtonName) && canDoubleJump && !isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            canDoubleJump = false;
            anim.SetTrigger("isDoubleJump");
        }

        rb.velocity += new Vector2(0, extraGravity * Physics2D.gravity.y * Time.deltaTime);
    }

    protected void wallJump()
    {
        if (isGround)
        {
            return;
        }
        if (wc.isOnWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);

            if (Input.GetButtonDown(ButtonName))
            {
                canDoubleJump = true;
                rb.velocity = new Vector2(-1 * wallJumpSpeed * tr.localScale.x, wallJumpSpeed);
                StartCoroutine(WallJumping());
            }
        }
    }
    IEnumerator WallJumping()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(0.5f);
        isWallJumping = false;
    }

    protected void CheckIsJump()
    {
        if (rb.velocity.y > 0.3f)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    protected void CheckOnGround()
    {
        isGround = Physics2D.Raycast(tr.position, Vector2.down, 1.2f, GroundLayer);
    }

    protected void SendVars()
    {
        anim.SetBool("isGround", isGround);
        anim.SetBool("isJump", isJump);
    }
}
