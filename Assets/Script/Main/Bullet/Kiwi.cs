using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwi : MonoBehaviour
{
    public BulletAttr attr;

    private Transform tr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        attr = GetComponent<BulletAttr>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tr.position.x < -64 || tr.position.x > 64)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            collision.GetComponent<PlayerBaseAttribute>().TakeDamage(attr.damage);
            // Debug.Log("Bullet Hit Player. Damage: " + attr.damage);
            Destroy(gameObject);
        }
    }
}
