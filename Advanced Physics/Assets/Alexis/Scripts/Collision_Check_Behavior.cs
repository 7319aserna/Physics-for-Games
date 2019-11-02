using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Check_Behavior : MonoBehaviour
{
    public bool Has_Collision_Occured = false;

    private void OnCollisionEnter(Collision collision) { Has_Collision_Occured = true; }

    private void OnCollisionExit(Collision collision) { Has_Collision_Occured = false; }
}
