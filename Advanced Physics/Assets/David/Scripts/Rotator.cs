using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Transform[] children;
    public float timeScale;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i < children.Length; i++)
        {
            children[i].localPosition = new Vector3(Mathf.Cos(Time.time / timeScale * children[i - 1].position.x), Mathf.Sin(Time.time / timeScale * children[i - 1].position.y), Mathf.Sin(Time.time / timeScale * Mathf.Cos(Time.time / timeScale * children[i - 1].position.z))) * i * Time.time;
        }
    }
}
