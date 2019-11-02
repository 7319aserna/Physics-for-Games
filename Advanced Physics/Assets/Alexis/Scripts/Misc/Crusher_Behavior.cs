using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher_Behavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) { if(collision.gameObject == GameObject.FindGameObjectWithTag("Player")) { collision.gameObject.GetComponent<Controller>().die(); } }
}
