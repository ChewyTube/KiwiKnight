using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAttr : MonoBehaviour
{
    public enum SlotType
    {
        Weapon,
        Blood,
        Bullet,
    }

    public Weapon weapon = new Weapon(
            WeaponAttr.Material.Wooden,
            WeaponAttr.WeaponType.Sword
            );
    public SlotType type;
    public float addBlood = 0;
    public BulletType bulletType = BulletType.Kiwi;


    // Start is called before the first frame update
    void Start()
    {
        // weapon 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
