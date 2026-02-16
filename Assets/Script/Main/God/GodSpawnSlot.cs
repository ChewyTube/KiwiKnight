using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodSpawnSlot : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private GameObject worldAttr;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private float spawnDeltaSpeed; 
    [SerializeField] private float destroyTime = 20.0f;

    [Header("权重")]
    [SerializeField] private float bloodWeight  = 0.1f;
    [SerializeField] private float weaponWeight = 0.7f;
    [SerializeField] private float bulletWeight = 0.2f;
    [SerializeField] private float kiwiWeight       = 0.7f;
    [SerializeField] private float fireballWeight   = 0.3f;

    [Header("血量范围")]
    [SerializeField] private float minBlood = 3.0f;
    [SerializeField] private float maxBlood = 13.0f;

    enum SlotType
    {
        Blood,
        Weapon,
        Bullet,
    }

    private Transform   tr;
    private Rigidbody2D rb;
    private WorldAttr   attr;

    private bool canSpawn = true;

    private static readonly System.Random _random = new System.Random(Guid.NewGuid().GetHashCode());

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform  >();
        rb = GetComponent<Rigidbody2D>();
        attr = worldAttr.GetComponent<WorldAttr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("]"))
        {
            SpawnOneNewSlot();
        }

        if (!canSpawn || attr.GetCurrentTime() == attr.GetTimePerRound())
        {
            return;
        }
        if (attr.GetCurrentTime() == 10)
        {
            //Debug.Log("spawn from 1");
            StartCoroutine(SpawnSomeNewSlots(3));
            canSpawn = false;
        }
        if(attr.GetCurrentTime() % 20 == 0)
        {
            //Debug.Log("spawn from 2");
            StartCoroutine(SpawnSomeNewSlots(2));
            canSpawn = false;
        }
        if(attr.GetCurrentTime() % 60 == 0)
        {
            //Debug.Log("spawn from 3");
            // Debug.Log(attr.GetCurrentTime());
            StartCoroutine(SpawnSomeNewSlots(5));
            canSpawn = false;
        }
        if(attr.GetCurrentTime() == attr.GetTimePerRound() - 5)
        {
            //Debug.Log("spawn from 4");
            StartCoroutine(SpawnSomeNewSlots(6));
            canSpawn = false;
        }
    }

    private IEnumerator SpawnSomeNewSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnOneNewSlot();
            yield return new WaitForSeconds(0.5f);
        }
        canSpawn = true;
    }
    private void SpawnOneNewSlot()
    {
        GameObject obj = Instantiate(
            spawnObject, transform.position, transform.rotation
            );
        int d = GetDirection();
        obj.GetComponent<Rigidbody2D>().position
            = transform.position + d * Vector3.right * 2;

        obj.GetComponent<Rigidbody2D>().velocity
            = Vector2.up * spawnSpeed + GetRandomUpVelocity()
            + d * (Vector2.right * spawnSpeed + GetRandomRightVelocity());
        obj.tag = "CloneSlot";
        StartCoroutine(DestroyAfterTime(destroyTime, obj));

        Texture2D texture = null;

        SlotType slotType = GetSlotType();

        if (slotType == SlotType.Blood)
        {
            obj.GetComponent<SlotAttr>().type = SlotAttr.SlotType.Blood;
            float blood = (float)_random.NextDouble() * (maxBlood - minBlood) + minBlood;
            obj.GetComponent<SlotAttr>().addBlood = blood;

            texture = Resources.Load<Texture2D>("Texture/Icons/blood");
        }
        else if(slotType == SlotType.Weapon)
        {
            obj.GetComponent<SlotAttr>().type = SlotAttr.SlotType.Weapon;

            WeaponAttr.WeaponType type = WeaponAttr.Instance.GetRandomWeaponType();
            WeaponAttr.Material mat = WeaponAttr.Instance.GetRandomMaterial();

            texture = Resources.Load<Texture2D>(GetTexturePath(type, mat));
            //Debug.Log(GetTexturePath(type, mat));

            obj.GetComponent<SlotAttr>().weapon.material = mat;
            obj.GetComponent<SlotAttr>().weapon.weaponType = type;
        }else if(slotType == SlotType.Bullet)
        {
            obj.GetComponent<SlotAttr>().type = SlotAttr.SlotType.Bullet;

            BulletType type = GetBulletType();
            obj.GetComponent<SlotAttr>().bulletType = type;

            texture = Resources.Load<Texture2D>(GetTexturePath(type));
            //Debug.Log(GetTexturePath(type));
        }

        GameObject item = obj.transform.GetChild(0).gameObject;
        
        item.GetComponent<SpriteRenderer>().sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            16.0f
            );
    }
    

    IEnumerator DestroyAfterTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if(obj != null)
        {
            Destroy(obj);
        }
    }
    private string GetTexturePath(WeaponAttr.WeaponType type, WeaponAttr.Material mat)
    {
        string typeName = Enum.GetName(typeof(WeaponAttr.WeaponType), type).ToLower();
        string matName  = Enum.GetName(typeof(WeaponAttr.Material), mat).ToLower();
        return "Texture/Weapon/" + matName + "_" + typeName;
    }
    private string GetTexturePath(BulletType type)
    {
        string typeName = Enum.GetName(typeof(BulletType), type).ToLower();
        return "Texture/Bullet/" + typeName;
    }
    private Vector2 GetRandomUpVelocity()
    {
        float y = (float)_random.NextDouble() * spawnDeltaSpeed;
        return new Vector2(0, y);
    }
    private Vector2 GetRandomRightVelocity()
    {
        float x = (float)_random.NextDouble() * spawnDeltaSpeed;
        return new Vector2(x, 0);
    }
    private int GetDirection()
    {
        if (_random.NextDouble() <= 0.5d)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    private SlotType GetSlotType()
    {
        double x = _random.NextDouble();

        if(x <= bloodWeight)
        {
            return SlotType.Blood;
        }else if(x <= bloodWeight + weaponWeight){
            return SlotType.Weapon;
        }
        else
        {
            return SlotType.Bullet;
        }
    }
    private BulletType GetBulletType()
    {
        double x = _random.NextDouble();
        if(x <= kiwiWeight)
        {
            return BulletType.Kiwi;
        }
        else
        {
            return BulletType.FireBall;
        }
    }
}
