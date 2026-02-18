using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float damage = 8f;
    [SerializeField] private float speedBonus = 0.5f;
 
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CloneSlot")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if(other.tag == "TNT")
        {
            GameObject smoke = Instantiate(explosion, transform.position, Quaternion.identity);
            smoke.GetComponent<Transform>().localScale *= 4;
            Destroy(other);
            Destroy(gameObject);
        }
        if(other.tag == "Player1" || other.tag == "Player2")
        {
            GameObject smoke = Instantiate(explosion, transform.position, Quaternion.identity);
            smoke.GetComponent<Transform>().localScale *= 2;
            
            PlayerBaseAttribute attr = other.GetComponent<PlayerBaseAttribute>();
            attr.TakeDamage(damage + rb.velocity.magnitude * speedBonus);

            Destroy(gameObject);
        }
    }
}
