using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponBase : MonoBehaviour
{
    protected Transform tr;
    protected Transform parentTr;
    protected SpriteRenderer    sr;
    private PlayerBaseAttribute  attr;
    private PlayerBaseThrow      Throw;
    protected Vector3 bottomLeft;
    protected Vector3 topRight;
    protected Vector3 initOffset;
    protected Vector2 size;
    protected Vector3 diagonal;

    [Header("Idel")]
    [SerializeField] protected float rotateSpeed;
    [SerializeField] protected float rotateCycle;

    [Header("Attack")]
    [SerializeField] protected string buttonName;
    [SerializeField] protected float[] angles = { 60f, -70f, 10f };
    [SerializeField] protected float[] speeds = { 200f, 800f, 30f };

    private float[] realAngles;
    private float[] realSpeeds;

    protected float rotatedTime;
    [Header("Debug")]
    [SerializeField] protected float initAngle;

    protected Bounds b;

    protected bool isAttacking;
    protected float initRotation;
    protected float initPositionY;
    private bool isBackingToIdel = false;

    private bool firstFrame = true;

    protected enum State
    {
        Idel,
        Attack,
        BackToIdel,
    }
    protected State s;

    // Start is called before the first frame update
    protected void Start()
    {
        GetComponent();
        GetSize();

        initRotation = tr.eulerAngles.z - 360f;
        initPositionY = tr.position.y;
        // Debug.Log("initPositionY:" + initPositionY);
        InitAnglesAndSpeeds();
        // Debug.Log("initRotation:" + initRotation);
    }

    private void GetVars()
    {
        Vector3 posParent = parentTr.position;
        Vector3 posWeapon = tr.position;
        GetCorner();
        initOffset = bottomLeft - parentTr.position;
    }

    private void InitAnglesAndSpeeds()
    {
        realAngles = new float[angles.Length];
        realSpeeds = new float[speeds.Length];
        for (int i = 0; i < angles.Length; i++)
        {
            realAngles[i] = angles[i];
            realSpeeds[i] = speeds[i];
        }
    }
    private void UpdateAngles()
    {
        if (tr.parent.transform.localScale.x > 0)
        {
            for (int i = 0; i < angles.Length; i++)
            {
                realAngles[i] = angles[i];
            }
        }
        else
        {
            for (int i = 0; i < angles.Length; i++)
            {
                realAngles[i] = - angles[i];
            }
        }
    }

    private void GetComponent()
    {
        tr = GetComponent<Transform>();
        parentTr = tr.parent.transform;
        sr = GetComponent<SpriteRenderer>();
    }

    protected void GetSize()
    {
        float pixelWidth = sr.sprite.texture.width;
        float pixelHeight = sr.sprite.texture.height;

        float unitWidth = pixelWidth / sr.sprite.pixelsPerUnit;
        float unitHeight = pixelHeight / sr.sprite.pixelsPerUnit;

        float scaleX = tr.localScale.x;
        float scaleY = tr.localScale.y;

        size = new Vector2(unitWidth * scaleX, unitHeight * scaleY);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (firstFrame)
        {
            GetVars();
            firstFrame = false;
        }
        GetCorner();
        // DrawLine(tr.localRotation.z, bottomLeftWorld);
        UpdateState();
        UpdateAngles();

        if (s == State.Idel)
        {
            Idel();
        }
        else
        {
            StartCoroutine(ExecuteRotationSequence());
        }

        // Debug.DrawLine(bottomLeft, topRight, Color.red, 1f);
        GetCorner();
        Debug.DrawLine(parentTr.position, parentTr.position + GetOffset(), Color.blue, 1f);
        Debug.DrawLine(bottomLeft, parentTr.position, Color.green, 1f);
        Debug.DrawLine(parentTr.position, parentTr.position + diagonal / 2 + GetOffset(), Color.yellow, 1f);
        Debug.DrawLine(bottomLeft, bottomLeft + diagonal / 2, Color.magenta, 1f);
        BackToIdelPosition();
        // Debug.Log(diagonal);
    }
    protected virtual Vector3 GetOffset()
    {
        return new Vector3(
            initOffset.x * parentTr.localScale.x,
            initOffset.y,
            0
        );
    }
    protected virtual void BackToIdelPosition()
    {
        tr.position = parentTr.position + diagonal / 2 + GetOffset();
    }

    private void UpdateState()
    {
        if (Input.GetButtonDown(buttonName) /*&& !isAttacking*/)
        {
            s = State.Attack;
        }
        else 
        {
            s = State.Idel;
        }
    }

    private void Idel()
    {
        rotatedTime += Time.deltaTime;

        tr.RotateAround(bottomLeft,
            Vector3.forward,
            rotateSpeed * Time.deltaTime * math.cos(rotatedTime / rotateCycle * 2 * math.PI
            ));
    }

    protected virtual void GetCorner()
    {
        b = sr.bounds;

        if (parentTr.transform.localScale.x > 0)
        {
            Quaternion rotation = Quaternion.AngleAxis(tr.eulerAngles.z , Vector3.forward);
            Vector2 v = rotation * size;
            diagonal = new(v.x, v.y, 0);
        }
        else
        {
            Quaternion rotation = Quaternion.AngleAxis(tr.eulerAngles.z + 90f, Vector3.forward);
            Vector2 v = rotation * size;
            diagonal = new(v.x, v.y, 0);
        }

        bottomLeft = tr.position - diagonal / 2f;
        topRight = tr.position + diagonal / 2f;
    }

    //protected IEnumerator BackToIdel()
    //{
    //    isBackingToIdel = true;
    //    float currentRotation = tr.localRotation.z;
    //    float targetRotation = initRotation;

    //    // 计算需要旋转的角度差（考虑360°循环）[4,7](@ref)
    //    float angleDiff = Mathf.DeltaAngle(currentRotation, targetRotation);
    //    float duration = 0.5f; // 旋转持续时间
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        // 计算当前帧的旋转角度（线性插值）
    //        float t = elapsedTime / duration;
    //        float currentAngle = Mathf.Lerp(0, angleDiff, t);

    //        // 围绕左下角点旋转[4,7](@ref)
    //        tr.RotateAround(
    //            bottomLeftWorld,
    //            Vector3.forward,
    //            currentAngle - (tr.localRotation.z - currentRotation)
    //        );

    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    // 精确校准最终角度
    //    tr.RotateAround(
    //        bottomLeftWorld,
    //        Vector3.forward,
    //        angleDiff - (tr.eulerAngles.z - currentRotation)
    //    );

    //    s = State.Idel;
    //    isBackingToIdel = false;
    //}
    protected virtual void BackToIdelInstant()
    {
        float currentRotation = tr.eulerAngles.z;
        float targetRotation = initRotation;
        if (tr.parent.transform.localScale.x <= -1)
        {
            targetRotation = -targetRotation;
        }


        // Debug.Log("currentRotation:" + currentRotation);
        // Debug.Log("targetRotation:" + targetRotation);

        float deltaAngle = Mathf.DeltaAngle(currentRotation, targetRotation);
        // Debug.Log("deltaAngle:" + deltaAngle);

        if (Mathf.Abs(deltaAngle) > rotateSpeed * 0.37f)
        {
            tr.RotateAround(
                bottomLeft,
                Vector3.forward,
                deltaAngle
            );
        }
    }
    
    protected IEnumerator ExecuteRotationSequence()
    {
        isAttacking = true;
        for(int i = 0; i < realAngles.Length; i++) 
        {
            float angle = realAngles[i];
            float speed = realSpeeds[i];
            float targetAngle = Mathf.Abs(angle); 
            float rotated = 0f; 
            float sign = Mathf.Sign(angle);

            while (rotated < targetAngle)
            {
                float step = speed * Time.deltaTime; 
                if (rotated + step > targetAngle)
                {
                    step = targetAngle - rotated; // 最后一帧微调避免超量
                }

                transform.RotateAround(bottomLeft, Vector3.forward, sign * step);
                rotated += step;
                yield return null; 
            }
        }

        isAttacking = false;
        BackToIdelInstant();
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        attr = tr.parent.GetComponent<PlayerBaseAttribute>();
        Throw = tr.parent.GetComponent<PlayerBaseThrow>();
        
        if (isAttacking && (collision.gameObject.CompareTag("CloneSlot")))
        {
            // PlayerBaseAttribute attr = 
            
            SlotAttr slotAttr = collision.gameObject.GetComponent<SlotAttr>();
            if (slotAttr.type == SlotAttr.SlotType.Weapon && !attr.unableToCollect)
            {
                Weapon newWeapon = slotAttr.weapon;
                attr.ChangeWeapon(newWeapon);
            }
            if(slotAttr.type == SlotAttr.SlotType.Blood)
            {
                float blood = slotAttr.addBlood;
                tr.parent.GetComponent<PlayerBaseAttribute>().AddHealth(blood);
            }
            if(slotAttr.type == SlotAttr.SlotType.Bullet)
            {
                BulletType type = slotAttr.bulletType;
                Throw.SetBulletType(type);
            }

            Destroy(collision.gameObject);
            // Debug.Log(newWeapon);

            isAttacking = false;
        }
        if (isAttacking 
            && (    collision.gameObject.CompareTag("Player1") 
                ||  collision.gameObject.CompareTag("Player2")
               )
           )
        {
            collision.gameObject.GetComponent<PlayerBaseAttribute>()
                .TakeDamage(attr.attack * attr.attackFactor);
            isAttacking = false;
        }
        if (isAttacking && (collision.gameObject.CompareTag("CloneBullet")))
        {
            Destroy(collision.gameObject);
            isAttacking = false;
        }
    }

}
