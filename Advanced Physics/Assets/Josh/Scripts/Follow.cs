using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    GameObject player;
    GameObject camPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camPos = GameObject.Find("CamPos");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (camPos == null)
        {
            camPos = GameObject.Find("CamPos");
        }
        transform.position = camPos.transform.position;
        transform.forward = player.transform.forward;
    }
}
