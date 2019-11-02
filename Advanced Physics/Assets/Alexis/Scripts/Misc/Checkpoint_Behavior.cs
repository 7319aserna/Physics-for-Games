using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Behavior : MonoBehaviour
{
    private Race_Manager_Behavior rmb;

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 12.5f)
            {
                rmb = GameObject.Find("Checkpoints").GetComponent<Race_Manager_Behavior>();
                rmb.selected_New_Position = this.gameObject.transform.position;
            }
        }
    }
}
