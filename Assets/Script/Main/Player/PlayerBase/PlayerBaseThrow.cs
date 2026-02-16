using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseThrow : MonoBehaviour
{
    public  GameObject      bullet;
    [SerializeField] protected  string          buttonName;
    [SerializeField] protected  float           offset;
    [SerializeField] private    BulletType      initBulletType = BulletType.Kiwi;
    [SerializeField] protected  float           cooldownTime;
    public    BulletManager   manager;

    private Transform   tr;
    private Rigidbody2D rb;

    private bool            canThrow = true;
    private BulletType      bulletType;
    private BulletAttrData  attrData;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        bulletType = initBulletType;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(bulletType);
        if (Input.GetButtonDown(buttonName) && canThrow)
        {
            StartCoroutine(Throw());
        }
    }

    private void CreateBullet()
    {
        bullet = manager.GetBullet(bulletType);
        switch (bulletType)
        {
            case BulletType.Kiwi:
                CreateKiwiBullet();
                break;
            case BulletType.FireBall:
                CreateFireballBullet();
                break;
        }
    }

    private void CreateKiwiBullet()
    {
        GameObject b = Instantiate(
                    bullet,
                    tr.position + tr.right * offset * tr.localScale.x,
                    Quaternion.identity
                    );

        BulletAttr attr = bullet.GetComponent<BulletAttr>();
        attr.canMove = true;
        Vector2 v0 = rb.velocity;

        // Debug.Log(attr.GetData().speed);
        b.tag = "CloneBullet";
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(v0.x + attr.GetData().speed * tr.localScale.x, 0);
    }
    private void CreateFireballBullet()
    {
        Debug.Log(bullet);

        GameObject b = Instantiate(
                    bullet,
                    tr.position + tr.right * offset * tr.localScale.x,
                    Quaternion.identity
                    );

        BulletAttr attr = bullet.GetComponent<BulletAttr>();
        BulletAttr attrOfB = b.GetComponent<BulletAttr>();
        attrOfB.canMove = true;
        if (this.CompareTag("Player1"))
        {
            attrOfB.SetTarget(2);
        }
        else
        {
            attrOfB.SetTarget(1);
        }
            
        Vector2 v0 = rb.velocity;

        // Debug.Log(attr.GetData().speed);

        b.tag = "CloneBullet";
        b.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(v0.x + attr.GetData().speed * tr.localScale.x, 0);
        StartCoroutine(DestroyBullet(b, 15));
    }

    private IEnumerator Throw()
    {
        CreateBullet();
        canThrow = false;
        yield return new WaitForSeconds(cooldownTime);
        canThrow = true;
    }

    public void SetBulletType(BulletType type)
    {
        bulletType = type;
        bullet = manager.GetBullet(type);
    }

    private IEnumerator DestroyBullet(GameObject b, int time)
    {
        yield return new WaitForSeconds(time);

        Destroy(b);
    }
}
