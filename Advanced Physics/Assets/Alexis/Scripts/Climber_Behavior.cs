using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Platform_Class
{
    [HideInInspector]
    public bool Has_Timer_Commenced = false;
    [HideInInspector]
    public bool Has_Trap_Been_Activated = false;
    public float Time_To_Wait = 0f;
    [HideInInspector]
    public float Timer = 0f;
    public GameObject Platform_GO;
    public Transform Platform_Transform;
}

public class Climber_Behavior : MonoBehaviour
{
    private List<Transform> Platform_Transforms = new List<Transform>();
    private Rigidbody RB;
    private Transform Player_Transform;

    public float Distance_Between_Objects = 0f;
    public List<Platform_Class> Platform_List = new List<Platform_Class>();

    private void Start()
    {
        for (int SJ = 0; SJ < Platform_List.Count; SJ++)
        {
            RB = Platform_List[SJ].Platform_GO.GetComponent<Rigidbody>();
            RB.isKinematic = true;
        }
        Player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        for (int SJ = 0; SJ < Platform_List.Count; SJ++) { Activate_Or_Deactivate_Hinge_Joint(Platform_List[SJ].Time_To_Wait, SJ); }
        Platform_Reset(5f);
    }

    private void Activate_Or_Deactivate_Hinge_Joint(float _Time_To_Wait, int _Current_Index)
    {
        if (Vector3.Distance(Platform_List[_Current_Index].Platform_GO.transform.position, Player_Transform.position) < Distance_Between_Objects && !Platform_List[_Current_Index].Has_Trap_Been_Activated) { Platform_List[_Current_Index].Has_Timer_Commenced = true; }
        if (Platform_List[_Current_Index].Has_Timer_Commenced)
        {
            Platform_List[_Current_Index].Timer += Time.deltaTime;
            if (Platform_List[_Current_Index].Timer >= _Time_To_Wait)
            {
                Platform_List[_Current_Index].Has_Timer_Commenced = false;
                Platform_List[_Current_Index].Has_Trap_Been_Activated = true;
                Platform_List[_Current_Index].Timer = 0f;
                RB = Platform_List[_Current_Index].Platform_GO.GetComponent<Rigidbody>();
                RB.isKinematic = false;
            }
        }
    }

    private void Platform_Reset(float _Time_To_Wait)
    {
        for(int SJ = 0; SJ < Platform_List.Count; SJ++)
        {
            if (Platform_List[SJ].Has_Trap_Been_Activated)
            {
                Platform_List[SJ].Timer += Time.deltaTime;
                if (Platform_List[SJ].Timer >= _Time_To_Wait)
                {
                    RB = Platform_List[SJ].Platform_GO.GetComponent<Rigidbody>();
                    if (Vector3.Distance(Platform_List[SJ].Platform_GO.transform.position, Player_Transform.position) != Distance_Between_Objects && Platform_List[SJ].Has_Trap_Been_Activated)
                    {
                        Platform_List[SJ].Platform_GO.transform.position = Platform_List[SJ].Platform_Transform.position;
                        Platform_List[SJ].Platform_GO.transform.rotation = Platform_List[SJ].Platform_Transform.rotation;
                        Platform_List[SJ].Platform_GO.transform.localScale = Platform_List[SJ].Platform_Transform.localScale;
                        RB.isKinematic = true;
                    }
                    Platform_List[SJ].Has_Trap_Been_Activated = false;
                    Platform_List[SJ].Timer = 0f;
                }
            }
        }
    }
}
