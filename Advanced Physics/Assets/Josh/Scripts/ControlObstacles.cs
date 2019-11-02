using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContorlObstacles : MonoBehaviour
{
    [SerializeField] GameObject ragdoll;
    [SerializeField] Controller player;
    [SerializeField] RagdollJoints joints;

    [SerializeField] float explosiveForce = 500f;
    [SerializeField] float explosionRadius = 500f;

    private void OnGUI()
    {
        if (GUILayout.Button("Head"))
        {
            Explode(joints.head, new Vector3(0, 0, explosiveForce));
        } 
    }

    public void Explode(GameObject joints, Vector3 force)
    {
        player.die();
        Rigidbody rb = joints.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //rb.AddExplosionForce(explosiveForce, joints.transform.position, explosionRadius, 1f);
            rb.AddForceAtPosition(-force, joints.transform.position);
        }
    }
}
