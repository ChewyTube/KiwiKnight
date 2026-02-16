using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletAttrData
{
    public float speed;
    public float damage;
    public float damageBonus;
    public float coolDownTime;
}

public class BulletAttr : MonoBehaviour
{
    private float speed                  = 6.0f;
    public  float damage                 = 1.0f;
    private float damageBonus            = 0.05f;
    private float damageBonusThreshold   = 6.0f;

    private Transform tr;
    private Rigidbody2D rb;

    [SerializeField] BulletType initType = BulletType.Kiwi;
    [SerializeField] BulletManager manager;
    private BulletType      type;
    private BulletAttrData  data    = new();
    public  bool            canMove = false;

    private GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        type = initType;
        // Debug.Log(manager.GetMap().Keys.ToString());
        // Debug.Log(manager.GetMap().Values.ToString());
        manager.GetMap().TryGetValue(type, out data);
        // Debug.Log("BulletType:" + (BulletType)type);
        // Debug.Log("data " + data);
        // Debug.Log("speed " + data.speed);
        // Debug.Log("damage " + data.damage);
        InitParameters();
    }

    private void InitParameters()
    {
        speed = data.speed;
        damage = data.damage;
        damageBonus = data.damageBonus;
        damageBonusThreshold = speed;
    }

    // Update is called once per frame
    void Update()
    {
        switch(type)
        {
            case BulletType.Kiwi:
                DamageFromKiwi();
                break;
            case BulletType.FireBall:
                DamageFromFireBall();
                break;
        }
    }

    private void DamageFromKiwi()
    {
        if (rb.velocity.magnitude > damageBonusThreshold)
        {
            float f = (rb.velocity.magnitude - damageBonusThreshold) * damageBonus;
            // Debug.Log("Damage: " + f * f + damage);

            damage = f * f + data.damage;
        }
        else
        {
            damage = data.damage;
        }

        // Debug.Log("Damage: " + damage);

    }
    private void DamageFromFireBall()
    { 

    }

    public BulletAttrData GetData()
    {
        return data;
    }
    public void SetTarget(int index)
    {
        target = manager.GetPlayer(index);
    }
    public GameObject GetTarget()
    {
        return target;
    }
}
