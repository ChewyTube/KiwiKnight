using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool isOnWall;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerBaseMove move;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        move = GetComponentInParent<PlayerBaseMove>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("onWall", isOnWall);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(rb + " v x:" + rb.velocity.x);
        if (collision.CompareTag("Wall") && rb.velocity.magnitude > 1.0f + move.moveSpeed)
        {
            
            isOnWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isOnWall = false;
        }
    }
}
