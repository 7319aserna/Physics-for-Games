using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race_Manager_Behavior : MonoBehaviour
{
    private float countdown_Timer = 2.5f;

    private GameObject player_GameObject;

    [HideInInspector]
    public Vector3 selected_New_Position = Vector3.zero;

    void Start() { player_GameObject = GameObject.FindGameObjectWithTag("Player"); }

    void Update() { set_Player_At_Checkpoint(); }

    private void set_Player_At_Checkpoint()
    {
        if (player_GameObject.GetComponent<Controller>().is_Ragdoll_Dead)
        {
            countdown_Timer -= 1f * Time.deltaTime;
            if (countdown_Timer <= 0f && selected_New_Position != Vector3.zero) { countdown_Timer = 2.5f; player_GameObject.transform.position = selected_New_Position; }
        }
    }
}
