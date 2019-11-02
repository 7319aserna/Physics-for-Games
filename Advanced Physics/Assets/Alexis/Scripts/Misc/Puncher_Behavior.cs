using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher_Behavior : MonoBehaviour
{
    private bool has_Maximum_Timer_Been_Set = false;
    private bool has_Puncher_Extended = false;
    private bool has_Puncher_Reached_Mark = false;

    private float countdown_Timer = 2.5f;
    private float current_Timer = 0f;
    private float maximum_Length = 5f;
    private float maximum_Timer;

    private GameObject attached_GameObject;

    private void OnCollisionEnter(Collision collision) { if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")) { collision.gameObject.GetComponent<Controller>().die(); } }

    public void attach_GameObject(GameObject _selected_GameObject) { attached_GameObject = _selected_GameObject; }

    public void extend_And_Return()
    {
        if (!has_Puncher_Reached_Mark)
        {
            if (!has_Maximum_Timer_Been_Set) { has_Maximum_Timer_Been_Set = true; maximum_Timer = Random.Range(0f, 5f); }
            current_Timer += (1f * Time.deltaTime);
            if (current_Timer >= maximum_Timer)
            {
                if (attached_GameObject != null)
                {
                    if (!has_Puncher_Extended)
                    {
                        attached_GameObject.transform.position = new Vector3(attached_GameObject.transform.position.x + maximum_Length, attached_GameObject.transform.position.y, attached_GameObject.transform.position.z);
                        has_Puncher_Extended = true;
                    }
                }
                countdown_Timer -= 1f * Time.deltaTime;
                if (countdown_Timer <= 0f)
                {
                    attached_GameObject.transform.position = new Vector3(attached_GameObject.transform.position.x - maximum_Length, attached_GameObject.transform.position.y, attached_GameObject.transform.position.z);
                    countdown_Timer = 2.5f;
                    has_Puncher_Reached_Mark = true;
                }
            }
        }
        else { current_Timer = 0f; has_Maximum_Timer_Been_Set = false; has_Puncher_Extended = false; has_Puncher_Reached_Mark = false; maximum_Timer = 0f; }
    }
}
