using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Weapon : WeaponBase
{
    protected override void BackToIdelInstant()
    {
        float currentRotation = tr.eulerAngles.z;
        float targetRotation = base.initRotation;
        if (tr.parent.transform.localScale.x >= 1) // player2初始朝向和1相反
        {
            targetRotation = -targetRotation;
        }


        // Debug.Log("currentRotation:" + currentRotation);
        // Debug.Log("targetRotation:" + targetRotation);

        float deltaAngle = Mathf.DeltaAngle(currentRotation, targetRotation);
        // Debug.Log("deltaAngle:" + deltaAngle);

        if (Mathf.Abs(deltaAngle) > rotateSpeed)
        {
            tr.RotateAround(
                bottomLeft,
                Vector3.forward,
                deltaAngle
            );
        }
    }

    protected override void GetCorner()
    {
        b = sr.bounds;

        if (parentTr.transform.localScale.x > 0)
        {
            Quaternion rotation = Quaternion.AngleAxis(tr.eulerAngles.z, Vector3.forward);
            Vector2 v = rotation * (size);
            diagonal = new(v.x, v.y, 0);
        }
        else
        {
            Quaternion rotation = Quaternion.AngleAxis(tr.eulerAngles.z + 90f, Vector3.forward);
            Vector2 v = rotation * (- size);
            diagonal = new(v.x, v.y, 0);
        }

        bottomLeft = tr.position - diagonal / 2f;
        topRight = tr.position + diagonal / 2f;

        // Debug.DrawLine(bottomLeftWorld, topRightWorld, Color.red, 1f);
        Debug.DrawLine(tr.position, topRight, Color.red, 1f);
        Debug.DrawLine(tr.position, bottomLeft, Color.blue, 1f);
    }
    protected override Vector3 GetOffset()
    {
        return new Vector3(
            initOffset.x * parentTr.localScale.x * -1,
            initOffset.y,
            0
        );
    }   
}
