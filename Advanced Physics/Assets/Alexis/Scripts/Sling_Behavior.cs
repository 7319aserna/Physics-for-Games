using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Sling_Behavior : MonoBehaviour
{
    private bool Is_Sling_Currently_In_Use = false;
    private Controller Player_Controller;
    private float Default_Mass = 1f;
    private float Force = 0f;
    private float Timer = 0f;
    private GameObject Player_GameObject;
    private RagdollJoints RJ;
    private Rigidbody RB;
    private Spawner S;

    public Animator Slingshot_Guard_Animator;
    [HideInInspector]
    public bool Has_Player_Been_Flung = false;
    public float Distance_Between_Objects = 0f;
    public GameObject Sling_GameObject;
    void Start()
    {
        Player_GameObject = GameObject.FindGameObjectWithTag("Player");
        Player_Controller = Player_GameObject.GetComponent<Controller>();
        RB = Player_GameObject.GetComponent<Rigidbody>();
        RJ = Player_GameObject.GetComponent<RagdollJoints>();
        S = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && Vector3.Distance(Sling_GameObject.transform.position, Player_GameObject.transform.position) < Distance_Between_Objects)
        {
            Is_Sling_Currently_In_Use = true;
            Slingshot_Guard_Animator.SetTrigger("SG_Enable");
        }
        else if (Is_Sling_Currently_In_Use)
        {
            Force += Time.deltaTime * 56.25f;
            Debug.Log("Force: " + Force);
            if(Force > 200f) { Force = 200f; }

            if (Input.GetKeyUp(KeyCode.S))
            {
                Has_Player_Been_Flung = true;
                Is_Sling_Currently_In_Use = false;
                RB.mass = 0.001f;
                Slingshot_Guard_Animator.SetTrigger("SG_Disable");
            }
        }

        if (Has_Player_Been_Flung)
        {
            if (Force > 125f) { S.Explode(RJ.chest, new Vector3(0f, 0f, -Force)); }
            else { Force = 0f; Has_Player_Been_Flung = false; RB.mass = Default_Mass; }
        }
    }
}
