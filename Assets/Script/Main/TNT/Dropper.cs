using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    #region Serialized Fields
    [Header("Dropper")]
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float initAngle = 0f;
    [SerializeField] private Side side;

    [Header("TNT")]
    [SerializeField] private float TNTOffset = 1f;
    [SerializeField] private float TNTVelocity = 10f;

    [SerializeField] private GameObject TNTPrefab;

    [Header("Manager")]
    [SerializeField] private GameObject GO_Manager;
    #endregion

    public Vector3 rotationAxis = new Vector3(0, 0, 1);

    private Vector3 initPosition;
    private Quaternion initRotation;

    private Transform tr;
    // private TNTTriggerManager manager;

    private bool shouldEmit = false;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        initPosition = tr.position;
        initRotation = tr.rotation;

        // manager = GO_Manager.GetComponent<TNTTriggerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float angleOffset = Mathf.Sin(Time.time * frequency * 2 * Mathf.PI) * amplitude;

        Quaternion targetRotation = initRotation * Quaternion.Euler(rotationAxis * angleOffset);

        transform.rotation = targetRotation;

        //if(side == Side.Left && manager.IsLeftTriggered())
        //{
        //    GenerateTNT();
        //    manager.ResetTriggerLeft();
        //}
        //else if(side == Side.Right && manager.IsRightTriggered())
        //{
        //    GenerateTNT();
        //    manager.ResetTriggerRight();
        //}
        if(shouldEmit)
        {
            GenerateTNT();
            shouldEmit = false;
        }
    }

    private void GenerateTNT()
    {
        GameObject newTNT = Instantiate(
            TNTPrefab,
            tr.position + TNTOffset * tr.rotation.eulerAngles,
            tr.rotation);

        Rigidbody2D rb = newTNT.GetComponent<Rigidbody2D>();
        rb.velocity = GetVelocityDirection(tr.rotation.eulerAngles) * TNTVelocity;
        // Debug.Log(rb.velocity);

        StartCoroutine(DestroyAfterTime(8f, newTNT));
    }

    private Vector2 GetVelocityDirection(Vector3 rotation)
    {
        if (side == Side.Left)
        {
            return new Vector2(Mathf.Cos(rotation.z / 180 * Mathf.PI), Mathf.Sin(rotation.z / 180 * Mathf.PI));
        }
        else if (side == Side.Right)
        {
            return new Vector2(Mathf.Cos((rotation.z + 180) / 180 * Mathf.PI), Mathf.Sin((rotation.z + 180) / 180 * Mathf.PI));
        }
        
        return Vector2.zero;
    }

    public void Emit()
    {
        shouldEmit = true;
    }

    IEnumerator DestroyAfterTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
