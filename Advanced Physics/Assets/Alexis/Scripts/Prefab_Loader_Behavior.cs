using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Loader_Behavior : MonoBehaviour
{
    public List<GameObject> Prefab_List = new List<GameObject>();
    [HideInInspector]
    public string[] Prefab_String_Array = new string[] { "None", "Platform", "Ramp", "Starting Platform", "Tilting Platform" };
}