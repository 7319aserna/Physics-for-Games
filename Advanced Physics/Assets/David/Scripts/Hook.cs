using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    Vector3 target;
    public Vector3 Target { get => target; set => target = value; }
    public bool Connected { get => connected;}

    Vector3 localCoord;
    public bool connected;
    public bool shot;
    GameObject connectedBody;
    [SerializeField] float flySpeed;
    [SerializeField] Transform parent;
    BoxCollider collider;
    private void Start()
    {
        connected = false;
        target = parent.position;
        localCoord = new Vector3(0, 0, 0);
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (connected) target = connectedBody.transform.position + localCoord;
        else if(!shot) target = parent.position;

        if (transform.position == target && !connected) { target = parent.position; detach(); }
        else if (transform.position == target) return;

        transform.position = Vector3.Lerp(transform.position, target, flySpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9) return;
        connected = true;
        localCoord = collision.collider.gameObject.transform.InverseTransformPoint(collision.GetContact(0).point);
        connectedBody = collision.collider.gameObject;
    }

    public void shoot()
    {
        shot = true;
        collider.isTrigger = false;
    }

    public void detach()
    {
        target = parent.position;
        shot = false;
        connected = false;
        collider.isTrigger = true;
    }
}
