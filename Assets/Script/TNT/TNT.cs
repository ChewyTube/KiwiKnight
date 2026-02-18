using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float damage = 8f;
    [SerializeField] private float speedBonus = 0.5f;

    [SerializeField] private float shakeDuration = 0.5f;  // 振动持续时间
    [SerializeField] private float shakeMagnitude = 0.1f; // 振动幅度
    [SerializeField] private float delay = 0.5f;

    private Vector3 originalPosition;   // 摄像机初始位置

    private Rigidbody2D rb;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        originalPosition = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool explode = false;
        if (other.tag == "CloneSlot")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            explode = true;
            Destroy(gameObject);
        }
        if(other.tag == "TNT")
        {
            GameObject smoke = Instantiate(explosion, transform.position, Quaternion.identity);
            smoke.GetComponent<Transform>().localScale *= 4;
            Destroy(other);
            explode = true;
            Destroy(gameObject);
        }
        if(other.tag == "Player1" || other.tag == "Player2")
        {
            GameObject smoke = Instantiate(explosion, transform.position, Quaternion.identity);
            smoke.GetComponent<Transform>().localScale *= 2;
            
            PlayerBaseAttribute attr = other.GetComponent<PlayerBaseAttribute>();
            attr.TakeDamage(damage + rb.velocity.magnitude * speedBonus);

            explode = true;
            Destroy(gameObject);
        }
        if (explode)
        {
            //StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        yield return new WaitForSeconds(delay);

        while (elapsed < shakeDuration)
        {
            // 生成随机偏移
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // 应用偏移
            cam.transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 恢复初始位置
        cam.transform.localPosition = originalPosition;
    }
}
