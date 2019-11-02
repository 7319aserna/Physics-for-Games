using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Behavior : MonoBehaviour
{
    private Scene_Manager_Behavior SMB;

    void Start() { SMB = GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>(); }
    void Update() { transform.Rotate(0f, 0f, 90f * Time.deltaTime); }

    private void OnCollisionEnter(Collision collision)
    {
        SMB.update_Counter();
        this.gameObject.SetActive(false);
    }
}
