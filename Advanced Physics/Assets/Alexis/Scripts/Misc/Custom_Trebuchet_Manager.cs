using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_Trebuchet_Manager : MonoBehaviour
{
    private float countdown_Timer = 7.5f;

    private GameObject go;
    private GameObject player_GameObject;

    private Scene_Manager_Behavior smb;

    private TrebuchetManager tm;

    [HideInInspector]
    public bool is_Player_Within_Boundaries = false;

    public List<GameObject> camera_List = new List<GameObject>();

    private void Start()
    {
        go = this.transform.GetChild(8).gameObject;
        player_GameObject = GameObject.FindGameObjectWithTag("Player");
        smb = GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>();
        tm = GameObject.Find("Trebuchet Launcher").GetComponent<TrebuchetManager>();
    }
    private void Update()
    {
        if (Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 12.5f)
        {
            camera_List[0].SetActive(false);
            camera_List[1].SetActive(true);
            GameObject.Find("Player").GetComponent<Controller>().enable_Player_Controls = false;
            GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>().enable_Heads_Up_Display(2, GameObject.Find("Scene Manager").GetComponent<Scene_Manager_Behavior>().user_Interface_List);
            if (!tm.has_Trebuchet_Been_Launched)
            {
                // Left
                if (Input.GetKey(KeyCode.A)) { this.gameObject.transform.Rotate(new Vector3(0f, 90f * Time.deltaTime, 0f)); }
                // Right
                if (Input.GetKey(KeyCode.D)) { this.gameObject.transform.Rotate(new Vector3(0f, -90f * Time.deltaTime, 0f)); }
            }
            else
            {
                countdown_Timer -= 1f * Time.deltaTime;
                if(countdown_Timer <= 0f)
                {
                    camera_List[0].SetActive(true);
                    camera_List[1].SetActive(false);
                    GameObject.Find("Player").GetComponent<Controller>().enable_Player_Controls = true;
                    go.SetActive(false);
                    player_GameObject.transform.position = go.transform.position;
                    tm.has_Trebuchet_Been_Launched = false;
                }
            }
            is_Player_Within_Boundaries = true;
            player_GameObject.SetActive(false);
            smb.can_Game_Be_Restarted = false;
        }
        else { is_Player_Within_Boundaries = false; smb.can_Game_Be_Restarted = true; }
    }
}