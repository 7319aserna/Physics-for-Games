using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject ragdoll;
    [SerializeField] Controller player;
    [SerializeField] RagdollJoints joints;

    [SerializeField] float explosiveForce = 500f;
    [SerializeField] float explosionRadius = 500f;

    private void OnGUI()
    {
        if (GUILayout.Button("Die"))
        {
            player.die();
        }

        if (GUILayout.Button("Start Over"))
        {
            Destroy(player.gameObject);
            GameObject newRag = (GameObject)Instantiate(ragdoll);
            player = newRag.GetComponent<Controller>();
            joints = newRag.GetComponent<RagdollJoints>();
            
        }

        if (GUILayout.Button("Head"))
        {
            Explode(joints.head, new Vector3(0, 0, explosiveForce));
        }

        if (GUILayout.Button("left arm"))
        {
            Explode(joints.leftArm, new Vector3(-explosiveForce * 2, 0, 0f));
        }

        if (GUILayout.Button("right arm"))
        {
            Explode(joints.rightArm, new Vector3(explosiveForce * 2, 0, 0f));
        }

        if (GUILayout.Button("left leg"))
        {
            Explode(joints.leftLeg, new Vector3(0, 0, explosiveForce));
        }

        if (GUILayout.Button("right leg"))
        {
            Explode(joints.rightLeg, new Vector3(0, 0, explosiveForce));
        }

        if (GUILayout.Button("chest"))
        {
            Explode(joints.chest, new Vector3 (0,0,explosiveForce));
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
