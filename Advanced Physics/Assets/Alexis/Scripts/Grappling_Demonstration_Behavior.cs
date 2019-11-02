using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling_Demonstration_Behavior : MonoBehaviour
{
    private bool has_Cooldown_Occured = false;
    private bool has_Trigger_Been_Activated = false;

    private BoxCollider bc;

    private float cooldown_Timer = 3f;
    private float countdown_Timer = 10f;

    public GameObject Player_GameObject_David;
    public GameObject Player_GameObject_Joshua;

    // Grab from the other platform
    public Grappling_Demonstration_Behavior GDB;

    private void Start() { bc = this.GetComponent<BoxCollider>(); }

    private void Update()
    {
        if (has_Trigger_Been_Activated)
        {
            countdown_Timer -= 1 * Time.deltaTime;
            Debug.Log(countdown_Timer);
            if(countdown_Timer <= 0f)
            {
                GDB.enabled = true;
                has_Cooldown_Occured = true;
                has_Trigger_Been_Activated = false;
                Player_GameObject_Joshua.transform.position = Player_GameObject_David.transform.position;
            }
        }
        if(!has_Trigger_Been_Activated)
        {
            bc.enabled = true;
            if (has_Cooldown_Occured)
            {
                cooldown_Timer -= 1f * Time.deltaTime;
                if(cooldown_Timer <= 0f)
                {
                    cooldown_Timer = 3f;
                    has_Cooldown_Occured = false;
                }
            }
            GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>().enable_Heads_Up_Display(0, GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>().user_Interface_List);
            Player_GameObject_David.SetActive(false);
            Player_GameObject_Joshua.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player_GameObject_David) { return; }
        else if (other.gameObject == Player_GameObject_Joshua && !has_Cooldown_Occured)
        {
            bc.enabled = false;
            countdown_Timer = 10.0f;
            GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>().enable_Heads_Up_Display(1, GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>().user_Interface_List);
            GDB.enabled = false;
            has_Trigger_Been_Activated = true;
            Player_GameObject_David.SetActive(true);
            Player_GameObject_Joshua.SetActive(false);
            Player_GameObject_David.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 5.0f, this.gameObject.transform.position.z);
        }
    }
}
