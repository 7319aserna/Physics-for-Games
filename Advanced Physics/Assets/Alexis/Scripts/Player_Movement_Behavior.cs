using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Behavior : MonoBehaviour
{
    private Rigidbody RB;

    public float Speed = 0f;

    // How high should the character jump?
    public int Jump = 0;
    void Start() { RB = this.GetComponent<Rigidbody>(); }

    // Update is called once per frame
    void Update()
    {
        float Rotation_Horizontal = Input.GetAxis("Horizontal");
        float Move_Vertical = Input.GetAxis("Vertical");

        Vector3 Direction = new Vector3(/* Move_Horizontal * Speed */ 0f, 0f, Move_Vertical * Speed);

        if (Rotation_Horizontal < 0) { Vector3 Rotation = new Vector3(0f, Rotation_Horizontal * (Speed / 2f), 0f); this.transform.Rotate(Rotation.x, Rotation.y, Rotation.z); }
        else { Vector3 Rotation = new Vector3(0f, Rotation_Horizontal * (Speed / 2f), 0f); this.transform.Rotate(Rotation.x, Rotation.y, Rotation.z); }

        if (Input.GetKeyDown(KeyCode.Space)) { Direction.y = Jump * Speed; }

        this.transform.position += /*Direction * Time.deltaTime;*/  Camera.main.transform.TransformDirection(Direction) * Time.deltaTime;
    }
}