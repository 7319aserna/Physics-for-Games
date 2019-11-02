using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Joint_Attachment_Behavior : MonoBehaviour
{
    private bool Has_Object_Been_Attached = false;
    private SpringJoint SP;
    private Transform Target;

    public float Distance_Between_Objects;
    public float Speed = 0f;
    void Start() { Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); }
    void Update()
    {
        float Move_Horizontal = Input.GetAxis("Horizontal");
        float Move_Vertical = Input.GetAxis("Vertical");

        Vector3 Direction = new Vector3(Move_Horizontal * Speed, 0f, Move_Vertical * Speed);

        this.transform.position += Direction * Time.deltaTime;
        //if(Vector3.Distance(this.gameObject.transform.position, Target.position) < Distance_Between_Objects && !Has_Object_Been_Attached)
        //{
        //    Has_Object_Been_Attached = true;
        //    SP = this.gameObject.AddComponent<SpringJoint>();
        //    SP.connectedBody = Target.gameObject.GetComponent<Rigidbody>();
        //    SP.anchor = new Vector3(0f, -1f, 0f);
        //    SP.connectedAnchor = Vector3.zero;
        //    SP.spring = 99f;
        //}
        //if (Input.GetKeyDown(KeyCode.E) && SP.connectedBody != null) { SP.connectedBody = null; }
    }
}
