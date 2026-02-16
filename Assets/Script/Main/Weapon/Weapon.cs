using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public WeaponAttr.Material material;
    public WeaponAttr.WeaponType weaponType;

    public Weapon(WeaponAttr.Material material, WeaponAttr.WeaponType weaponType)
    {
        this.material = material;
        this.weaponType = weaponType;
    }
    public Weapon()
    {
        this.material = WeaponAttr.Material.Wooden;
        this.weaponType = WeaponAttr.WeaponType.Sword;
    }

}
