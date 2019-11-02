using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ramp_Launcher_Behavior : MonoBehaviour
{
    private Controller player_Controller;

    private float countdown_Timer = 1f;
    private float launcher_Countdown_Timer = 1f;

    private int sprite_Counter = 0;

    private Renderer renderer_Object;

    public List<Sprite> ramp_Sprites = new List<Sprite>();

    void Start() { player_Controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>(); renderer_Object = this.gameObject.GetComponent<Renderer>(); }
    void Update() { update_Sprites(ramp_Sprites); }

    private void OnCollisionEnter(Collision collision) { ramp_Launcher(); }

    private void ramp_Launcher()
    {
        // player_Controller.die();
        player_Controller.GetComponent<Rigidbody>().velocity += transform.TransformDirection(Input.GetAxis("Horizontal") * player_Controller.rotateSpeed, player_Controller.GetComponent<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * player_Controller.speed - 200f);
    }

    private void update_Sprites(List<Sprite> _sprite_List)
    {
        countdown_Timer -= 1f * Time.deltaTime;

        if(countdown_Timer <= 0f)
        {
            countdown_Timer = 1f;
            renderer_Object.material.mainTexture = _sprite_List[sprite_Counter].texture;
            sprite_Counter += 1;
            if(sprite_Counter > 1) { sprite_Counter = 0; }
        }
    }
}
