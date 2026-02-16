using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public BulletAttr attr;

    private Transform tr;
    private Rigidbody2D rb;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        attr = GetComponent<BulletAttr>();

        target = attr.GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (tr.position.x < -32 || tr.position.x > 32)
        {
            attr.canMove = false;
            Destroy(gameObject);
        }

        if (this.CompareTag("CloneBullet"))
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 now = tr.position;
            Vector3 dir = (targetPosition - now).normalized;
            // tr.LookAt(target);
            rb.velocity = dir * attr.GetData().speed;
        }
        // Debug.Log(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(target.tag))
        {
            collision.GetComponent<PlayerBaseAttribute>().TakeDamage(attr.damage);
            // Debug.Log("Bullet Hit Player. Damage: " + attr.damage);
            Destroy(gameObject);
        }
    }
}
