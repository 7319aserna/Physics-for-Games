using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    //public Rigidbody leftRB;
    //public Rigidbody rightRB;
    public Rigidbody hips;
    Vector3 leftTarget;
    [SerializeField] Hook leftGrapple;
    Vector3 rightTarget;
    [SerializeField] Hook rightGrapple;
    [SerializeField] float grappleStrength;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float maxVel;
    [SerializeField] float breakStrength;
    [SerializeField] float grappleDistance;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            hips.AddForce(transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            hips.AddForce(transform.TransformDirection(Vector3.back) * moveSpeed * Time.deltaTime, ForceMode.Force);
        }

        if(Input.GetKey(KeyCode.A))
        {
            hips.AddForce(-transform.TransformDirection(Vector3.right) * moveSpeed * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            hips.AddForce(transform.TransformDirection(Vector3.right) * moveSpeed * Time.deltaTime, ForceMode.Force);
        }

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit target;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out target, grappleDistance))
            {
                Vector3 point = target.point;
                leftGrapple.Target = point;
            }
            else
            {
                leftGrapple.Target = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(grappleDistance);
            }
            leftGrapple.shoot();
        }
        else if (!Input.GetMouseButton(0))
        {
            leftGrapple.detach();

        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit target;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out target, grappleDistance);
            Vector3 point = target.point;
            rightGrapple.Target = point;
            rightGrapple.shoot();
        }
        else if (!Input.GetMouseButton(1))
        {
            rightGrapple.detach();
        }
        //Debug.Log(leftTarget - transform.position);

        Vector3 lookVec = Camera.main.transform.position + (Camera.main.transform.TransformDirection(Vector3.forward) * 500);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) + new Vector3(0, -0.001f, 0))) lookVec.y = 0;
        transform.rotation = Quaternion.LookRotation(lookVec, Vector3.up);
    }

    private void FixedUpdate()
    {
        if (!(leftGrapple.Connected || rightGrapple.Connected)) return;
        hips.AddForce((rightGrapple.Target - transform.position) * grappleStrength * Time.deltaTime);
        hips.AddForce((leftGrapple.Target - transform.position) * grappleStrength * Time.deltaTime);
        if (hips.velocity.magnitude > maxVel)
        {
            hips.velocity = hips.velocity.normalized * maxVel;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            hips.velocity -= (hips.velocity.normalized * breakStrength);
        }
    }
}
