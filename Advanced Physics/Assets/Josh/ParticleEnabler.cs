using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnabler : MonoBehaviour
{


    public GameObject[] part;
    // Start is called before the first frame update
    void Start()
    {
        part = GameObject.FindGameObjectsWithTag("WaterSpray");

        foreach (var p in part)
        {
            StartCoroutine(PlayParticle(p, p.GetComponent<ParticleCollision>().delay));
        }
    }


    IEnumerator PlayParticle(GameObject p, float seconds)
    {
        while (true)
        {
            p.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(seconds);
        }
    }
}
