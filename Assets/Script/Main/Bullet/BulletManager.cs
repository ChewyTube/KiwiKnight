using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Kiwi,
    FireBall,
}

public class BulletManager : MonoBehaviour
{
    private void Awake()
    {
        map = new Dictionary<BulletType, BulletAttrData>
        {
            [BulletType.Kiwi] = kiwiData,
            [BulletType.FireBall] = fireBallData
        };
    }

    private Dictionary<BulletType, BulletAttrData> map 
        = new Dictionary<BulletType, BulletAttrData>();

    [SerializeField] private BulletAttrData kiwiData;
    [SerializeField] private BulletAttrData fireBallData;

    [SerializeField] private GameObject kiwi;
    [SerializeField] private GameObject fireBall;

    public GameObject Player1;
    public GameObject Player2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<BulletType, BulletAttrData> GetMap()
    {
        return map;
    }
    public GameObject GetBullet(BulletType type)
    {
        switch (type)
        {
            case BulletType.Kiwi:
                return kiwi;
            case BulletType.FireBall:
                return fireBall;
            default:
                throw  new System.Exception("BulletType not found");
        }
    }
    public GameObject GetPlayer(int index)
    {
        if (index == 1)
        {
            return Player1;
        }
        else if (index == 2)
        {
            return Player2;
        }
        else
        {
            throw new System.Exception("Player index not found");
        }
    }
}
