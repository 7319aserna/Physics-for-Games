using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Platform_Behavior : MonoBehaviour
{
    private bool Has_CB_Target_Been_Setup = false;

    [HideInInspector]
    public Connector_Behavior CB_Target;

    private void Update()
    {
        if (CB_Target == null && Has_CB_Target_Been_Setup)
        {
            DestroyImmediate(this.gameObject, true);
            Has_CB_Target_Been_Setup = false;
        }
    }

    public Connector_Behavior Set_CB_Target(GameObject _CB_Target)
    {
        CB_Target = _CB_Target.GetComponent<Connector_Behavior>();
        Has_CB_Target_Been_Setup = true;
        return CB_Target;
    }

    public Vector3 Connector_Offset(GameObject _Connector, GameObject _Connector_Target)
    {
        Vector3 Distance_Between_Connectors = new Vector3(_Connector.transform.position.x - _Connector_Target.transform.position.x, _Connector.transform.position.y - _Connector_Target.transform.position.y, _Connector.transform.position.z - _Connector_Target.transform.position.z);
        return Distance_Between_Connectors;
    }
}
