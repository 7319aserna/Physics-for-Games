using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punching_Manager_Behavior : MonoBehaviour
{
    public List<GameObject> punching_List = new List<GameObject>();

    void Start()
    {
        for (int SJ = 0; SJ < punching_List.Count; SJ++) { punching_List[SJ].AddComponent<Puncher_Behavior>(); }
        for (int SJ = 0; SJ < punching_List.Count; SJ++) { punching_List[SJ].GetComponent<Puncher_Behavior>().attach_GameObject(punching_List[SJ]); }
    }

    void Update() { for(int SJ = 0; SJ < punching_List.Count; SJ++) { punching_List[SJ].GetComponent<Puncher_Behavior>().extend_And_Return(); } }
}
