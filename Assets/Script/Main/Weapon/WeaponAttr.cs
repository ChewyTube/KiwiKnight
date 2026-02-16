using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttr : MonoBehaviour
{
    private static WeaponAttr _instance;
    public static WeaponAttr Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("WeaponAttr");
                _instance = obj.AddComponent<WeaponAttr>();
                DontDestroyOnLoad(obj); // 跨场景保留
            }
            return _instance;
        }
    }

    public enum WeaponType
    {
        Sword,
        Axe,
    }

    public enum Material
    {
        Wooden,
        Stone,
        Iron,
        Golden,
        Diamond,
        Netherite,
    }

    private Dictionary<Material, float> damages = new();
    private static readonly System.Random _sharedRandom = new System.Random(Guid.NewGuid().GetHashCode());
    //private float speedBonus;
    //private float damageBonus;
    //private float speed;
    //private float damage;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // 避免重复创建
        }

        InitDamages();
    }

    private void InitDamages()
    {
        damages.Add(Material.Wooden     , 1.0f);
        damages.Add(Material.Stone      , 1.2f);
        damages.Add(Material.Iron       , 1.5f);
        damages.Add(Material.Golden     , 2.0f);
        damages.Add(Material.Diamond    , 2.6f);
        damages.Add(Material.Netherite  , 3.0f);
    }

    public float GetDamage(Material material)
    {
        return damages.ContainsKey(material) ? damages[material] : 0f;
    }

    public float GetDamageBonus(WeaponType weaponType)
    {
        return weaponType == WeaponType.Sword? 1.0f : 1.3f;
    }

    public WeaponType GetRandomWeaponType()
    {
        int length = Enum.GetValues(typeof(WeaponType)).Length;
        return (WeaponType)_sharedRandom.Next(0, length);
    }

    public Material GetRandomMaterial()
    {
        int length = Enum.GetValues(typeof(Material)).Length;
        return (Material)_sharedRandom.Next(0, length);
    }

}