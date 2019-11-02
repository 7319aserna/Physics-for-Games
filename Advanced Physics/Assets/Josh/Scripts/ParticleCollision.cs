using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public ParticleSystem part;
    public float delay;
    public List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;
        //print(numCollisionEvents);
        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                //print(force/100);
                if (rb.GetComponent<Controller>() != null) { if (!rb.GetComponent<Controller>().is_Invincibility_Active) { rb.GetComponent<Controller>().die(); } }
                rb.AddForce(force);
            }
            i++;
        }
    }
}
