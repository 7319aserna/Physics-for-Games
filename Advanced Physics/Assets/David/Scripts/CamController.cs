using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;
    public float lookSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + camOffset, Time.deltaTime * 5);
        float lookDeltaX = Input.GetAxis("Mouse X");
        Debug.Log(lookDeltaX);
        float lookDeltaY = -Input.GetAxis("Mouse Y");
        Debug.Log(lookDeltaY);
        Quaternion rotY = Quaternion.AngleAxis(lookDeltaX * lookSpeed, Vector3.up);
        Quaternion rotX = Quaternion.AngleAxis(lookDeltaY * lookSpeed, Vector3.right);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Camera.main.transform.rotation * rotX, .1f);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * rotY, 0.1f);
    }
}
