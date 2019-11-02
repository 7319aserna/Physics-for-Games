using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetRelease : MonoBehaviour
{
    Rigidbody[] rb;
    private void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        for (int i = 4; i < rb.Length; i++)
        {
            rb[i].constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void Fire()
    {
        for (int i = 4; i < rb.Length; i++)
        {
            rb[i].constraints = RigidbodyConstraints.None;
        }
    }
}
