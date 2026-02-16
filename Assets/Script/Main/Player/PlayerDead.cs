using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private PlayerBaseAttribute attr;
    private Animator anim;
    private Transform tr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        attr = GetComponent<PlayerBaseAttribute>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(tr.position.y);
        if (attr.health <= 0 || tr.position.y <= -32f)
        {
            attr.deadCount += 1;
            anim.SetTrigger("isDead");
            attr.health = attr.maxHealth;
            tr.position = new Vector3(0, -20f, 0);
            rb.velocity = Vector2.zero;

            Debug.Log("Player " + attr.deadCount);
        }
    }

    public void Rebirth()
    {
        tr.position = attr.respawnPos;
        attr.health = attr.maxHealth;
        // Debug.Log("Rebirth " + this.gameObject.name + " at " + attr.respawnPos.ToString());
    }
}
