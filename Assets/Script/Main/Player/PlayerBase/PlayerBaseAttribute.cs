using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseAttribute : MonoBehaviour
{

    public float    maxHealth       = 20;
    public float    health;
    public float    attack          = 2;
    public float    maxEnergy       = 20;
    public float    energy;
    public float    expirence       = 0;
    public float    attackFactor    = 1.0f;
    public float    defenceFactor   = 1.0f;
    public Vector2  respawnPos;
    public int      deadCount       = 0;

    public Weapon weapon;

    public bool unableToCollect = false;

    public Slider           healthSlider;
    public TextMeshProUGUI  healthText;
    public TextMeshProUGUI  deadCountText;
    [SerializeField] protected GameObject       weaponObj;
    [SerializeField] protected float            speedBonus          = 0.1f;
    [SerializeField] protected float            speedBonusThreshold = 10.0f;
    [SerializeField] protected string           unableToCollectBotton;
    public Image            unableToCollectImage;
 
    protected Rigidbody2D rb;
    protected Transform   tr;

    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
        energy = maxEnergy;

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

        InitWeapon();
        InitWeaponTexture();
    }

    // Update is called once per frame
    protected void Update()
    {
        UpdateAttr();

        healthSlider.value = health / maxHealth;
        healthText.text = "Health: " + health.ToString("F1");
        deadCountText.SetText(deadCount.ToString());
        // Debug.Log(deadCountText.text + deadCount.ToString());
        // Debug.Log("velocity: " + rb.velocity.magnitude + " onject name:" + gameObject.name);
        if (rb.velocity.magnitude > speedBonusThreshold)
        {
            attackFactor = 1.0f + (rb.velocity.magnitude - speedBonusThreshold) * speedBonus;
        }
        else
        {
            attackFactor = 1.0f;
        }

        if (Input.GetButtonDown(unableToCollectBotton))
        {
            unableToCollect = !unableToCollect;
        }
        UpdateLock();
    }

    private void UpdateLock()
    {
        unableToCollectImage.enabled = unableToCollect;
    }

    public void TakeDamage(float damage)
    {
        health -= damage / defenceFactor;
    }
    public void AddHealth(float health)
    {
        this.health += health;
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }

    public void InitWeapon()
    {
        weapon = new Weapon(WeaponAttr.Material.Wooden, WeaponAttr.WeaponType.Sword);
    }
    private void InitWeaponTexture()
    {
        string texturePath = GetTexturePath(weapon.weaponType, weapon.material);
        Texture2D texture = Resources.Load<Texture2D>(
            texturePath
            );
        weaponObj.GetComponent<SpriteRenderer>().sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                16.0f
                );
    }
    private string GetTexturePath(WeaponAttr.WeaponType type, WeaponAttr.Material mat)
    {
        string typeName = Enum.GetName(typeof(WeaponAttr.WeaponType), type).ToLower();
        string matName = Enum.GetName(typeof(WeaponAttr.Material), mat).ToLower();
        return "Texture/Weapon/" + matName + "_" + typeName;
    }
    private void UpdateAttr()
    {
        attack = WeaponAttr.Instance.GetDamage(weapon.material);
        attackFactor = WeaponAttr.Instance.GetDamageBonus(weapon.weaponType);
    }
    public void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        InitWeaponTexture();
    }
    public void InitBullet()
    {
        var Throw = GetComponent<PlayerBaseThrow>();
        Throw.SetBulletType(BulletType.Kiwi);
    }
}
