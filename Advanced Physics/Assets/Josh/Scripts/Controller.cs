using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float rotateSpeed = 2f;
    Animator anim;
    Rigidbody rb;

    //[HideInInspector]
    public bool canJump;

    bool jump;
    float hori, vert;

    public float lowMult,fallMult,jumpHeight;

    public ParticleSystem part;
    [HideInInspector]
    public List<ParticleCollisionEvent> collisionEvents;

    // Alexis's Code --- Begin
    private TrebuchetManager tm;

    private float invincible_Countdown = 1f;
    private float countdown_Timer = 3f;

    [HideInInspector]
    public bool enable_Player_Controls = true;
    [HideInInspector]
    public bool is_Invincibility_Active = false;
    
    [HideInInspector]
    public bool is_Ragdoll_Dead = false;
    // Alexis's Code --- End

    // Start is called before the first frame update
    void Start()
    {
        //part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        setRigidbodyState(true);
        setColliderState(false);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        canJump = true;

        tm = GameObject.Find("Trebuchet Launcher").GetComponent<TrebuchetManager>();
    }

    private void Update()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        if (jump)
        {
            if (!canJump)
            {
                //Vector3 down = transform.TransformDirection(Vector3.down);
                jump = false;
                //if (Physics.Raycast(transform.position, down, 1f))
                //{
                //    canJump = true;
                //    Debug.DrawRay(transform.position, transform.TransformDirection(down) * 5, Color.black);
                //    jump = true;
                //}
            }
        }

        // Alexis's Code --- Begin
        if (is_Ragdoll_Dead)
        {
            countdown_Timer -= 1f * Time.deltaTime;
            if (countdown_Timer <= 0f) { countdown_Timer = 3f; live(); }
        }
        invincibility_Countdown();
        // Alexis's Code --- End
    }

    private void FixedUpdate()
    {
        if (enable_Player_Controls)
        {
            Vector3 movement = new Vector3(hori * rotateSpeed, rb.velocity.y, vert * speed);

            if (vert > 0f)
            {
                movement = transform.TransformDirection(movement);
                //rb.velocity = new Vector3(hori * speed, rb.velocity.y, vert * speed);
                rb.velocity = movement;
            }
            else if (vert < 0f)
            {
                movement.z /= 3;
                movement = transform.TransformDirection(movement);
                //rb.velocity = new Vector3(hori * speed, rb.velocity.y, vert * speed/3);
                rb.velocity = movement;
            }

            if (jump)
            {
                anim.SetBool("Jump", true);
                //rb.AddForce(transform.up * jumpHeight, ForceMode.Acceleration);
                rb.velocity = Vector3.up * jumpHeight;
                canJump = false;
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowMult - 1) * Time.deltaTime;
            }

            //anim.SetFloat("Turn", rb.velocity.x);
            anim.SetFloat("Speed", vert * speed);

            transform.Rotate(0, hori * rotateSpeed, 0);
        }
    }

    public void die()
    {
        //Destroy(gameObject, 3f);
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);

        is_Ragdoll_Dead = true;
    }
    public void live()
    {
        GetComponent<Animator>().enabled = true;
        setRigidbodyState(true);
        setColliderState(false);

        is_Invincibility_Active = true;
        is_Ragdoll_Dead = false;
        rb.velocity += transform.TransformDirection(hori * rotateSpeed, rb.velocity.y, vert * speed - 12.5f);

    }

    public void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in  colliders)
        {
            col.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
    
    // Alexis's Functions --- Begin
    private void invincibility_Countdown()
    {
        if (is_Invincibility_Active)
        {
            invincible_Countdown -= 1f * Time.deltaTime;
            if(invincible_Countdown <= 0f)
            {
                invincible_Countdown = 1f;
                is_Invincibility_Active = false;
            }
        }
    }
    // Alexis's Functions --- End
}
