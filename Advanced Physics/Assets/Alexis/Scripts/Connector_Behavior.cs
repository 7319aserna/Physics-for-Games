using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

// PLAN: After this, have a 2 player where the user controls a hose and uses the mouse to spray the player
public class Connector_Behavior : MonoBehaviour
{
    #region Private
        #region Integer
        private int Connector_One_Previous_Index = -1;
        private int Connector_Two_Previous_Index = -1;
        #endregion 
    #endregion

    #region Public
        #region Bool
        [HideInInspector]
        // Connector attaches to platform
        public bool Attachment_Mode;
        [HideInInspector]
        public bool Attachment_Mode_Options;
        [HideInInspector]
        public bool Change_Connector_Offset;
        [HideInInspector]
        // Platform(s) connects to connector
        public bool Connection_Mode;
        [HideInInspector]
        public bool Enable_Port_One_Miscellaneous_Options;
        [HideInInspector]
        public bool Enable_Port_One_Transform_Options;
        [HideInInspector]
        public bool Enable_Port_Two_Transform_Options;
        [HideInInspector]
        public bool Has_Connector_Offset_Been_Found = false;
        [HideInInspector]
        public bool Has_New_Connector_Offset_Been_Found = false;
        [HideInInspector]
        public bool Has_Port_One_Connection_Occured = false;
        [HideInInspector]
        public bool Has_Port_Two_Connection_Occured = false;
        [HideInInspector]
        public bool Reset_Port_Connections_Position = false;
        #endregion

        #region GameObject
        [HideInInspector]
        public GameObject Attachment_Port_One;
        [HideInInspector]
        public GameObject Attachment_Port_Two;
        [HideInInspector]
        // CPO
        public GameObject Connection_Port_One;
        [HideInInspector]
        // CPT
        public GameObject Connection_Port_Two;
        #endregion

        #region Integer
        [HideInInspector]
        public int CPO_List_Index = 0;
        [HideInInspector]
        public int CPT_List_Index = 0;
        [HideInInspector]
        // COCP = Connector_One_Center_Point
        public int COCP_Index = -1;
        [HideInInspector]
        public int COPI = 0;
        [HideInInspector]
        // CTCP = Connector_Two_Center_Point
        public int CTCP_Index = -1;
        [HideInInspector]
        public int CTPI = 0;
        #endregion

        #region Platform Behavior / Prefab_Loader_Behavior
        [HideInInspector]
        public Platform_Behavior Attachment_Port_One_PB;
        [HideInInspector]
        public Platform_Behavior Attachment_Port_Two_PB;

        [HideInInspector]
        public Prefab_Loader_Behavior PLB;
        #endregion

        #region Transform
        [HideInInspector]
        public Transform Connector_One_Center_Point;
        [HideInInspector]
        public Transform Connector_Two_Center_Point;
        #endregion

        #region Vector3
        [HideInInspector]
        public Vector3 Connector_Offset;
        [HideInInspector]
        public Vector3 Connector_One_Translation_Offset;
        [HideInInspector]
        public Vector3 Connector_Two_Translation_Offset;
        #endregion
    #endregion

    // Connector Behavior Functions
    public bool Check_For_Change(int _Current_Index, int _Previous_Index)
    {
        bool Has_Change_Occured = false;

        if(_Current_Index != _Previous_Index) { Has_Change_Occured = true; _Previous_Index = _Current_Index; }
        else { Has_Change_Occured = false; }

        return Has_Change_Occured;
    }

    public Vector3 Change_Transform_Point(int _Current_Position, string _Current_Connector_Port, Vector3 _Current_Connector_Vector3)
    {
        if(_Current_Position < -1) { _Current_Position = -1; }
        _Current_Position += 1;

        switch (_Current_Connector_Port)
        {
            case "Connector Port One":
                int Platform_Connector_Capacity_One = Connection_Port_One.transform.Find("Platform Connectors").transform.childCount;
                if (_Current_Position > Platform_Connector_Capacity_One - 1) { _Current_Position = 0; }
                _Current_Connector_Vector3 = Connection_Port_One.transform.Find("Platform Connectors").transform.GetChild(_Current_Position).position;
                break;

            case "Connector Port Two":
                int Platform_Connector_Capacity_Two = Connection_Port_Two.transform.Find("Platform Connectors").transform.childCount;
                if (_Current_Position > Platform_Connector_Capacity_Two - 1) { _Current_Position = -1; }
                _Current_Connector_Vector3 = Connection_Port_Two.transform.Find("Platform Connectors").transform.GetChild(_Current_Position).position;
                break;
        }
        return _Current_Connector_Vector3;
    }

    public void Instantiate_Prefab(string _Selected_Connection_Port)
    {
        if (PLB != null) { PLB = GameObject.FindGameObjectWithTag("Scene Manager").GetComponent<Prefab_Loader_Behavior>(); }

        if (_Selected_Connection_Port == "Connection Port One")
        {
            if (CPO_List_Index != 0)
            {
                if (CPO_List_Index - 1 != Connector_One_Previous_Index)
                {
                    if (Connection_Port_One != null) { DestroyImmediate(Connection_Port_One, true); }
                    Connector_One_Previous_Index = -1;
                }

                if (Connector_One_Previous_Index != -1) { Connector_One_Previous_Index = -1; }

                if (Connector_One_Previous_Index == -1)
                {
                    Connector_One_Previous_Index = CPO_List_Index - 1;
                    Connection_Port_One = GameObject.Instantiate(PLB.Prefab_List[CPO_List_Index - 1]);
                }
            }
        }

        if (_Selected_Connection_Port == "Connection Port Two")
        {
            if (CPT_List_Index != 0)
            {
                if (CPT_List_Index - 1 != Connector_Two_Previous_Index)
                {
                    if (Connection_Port_Two != null) { DestroyImmediate(Connection_Port_Two, true); }
                    Connector_Two_Previous_Index = -1;
                }

                if (Connector_Two_Previous_Index != -1) { Connector_Two_Previous_Index = -1; }

                if (Connector_Two_Previous_Index == -1)
                {
                    Connector_Two_Previous_Index = CPT_List_Index - 1;
                    Connection_Port_Two = GameObject.Instantiate(PLB.Prefab_List[CPT_List_Index - 1]);
                }
            }
        }
    }

    public void Object_Rotation_Left_Or_Right(GameObject _Connection_Port, string _Left_Or_Right)
    {
        switch (_Left_Or_Right)
        {
            case "Left": _Connection_Port.transform.Rotate(0f, -45f, 0f); break;
            case "Right": _Connection_Port.transform.Rotate(0f, 45f, 0f); break;
        }
    }

   
}

#if UNITY_EDITOR
[CustomEditor(typeof(Connector_Behavior))]
[ExecuteInEditMode]
public class Button_Interaction_Behavior_Editor : Editor
{
    #region Private
    private bool Reset_Connector_Offset = false;
    #endregion

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Connector_Behavior CB = (Connector_Behavior)target;

        CB.Attachment_Mode = EditorGUILayout.Toggle("Attachment Mode", CB.Attachment_Mode);
        if (!CB.Attachment_Mode)
        {
            CB.Has_Connector_Offset_Been_Found = false;
            if (CB.Attachment_Port_One != null) { CB.Attachment_Port_One = null; }
        }
        else
        {
            if (CB.Connection_Port_One != null && CB.Connection_Port_Two != null) { CB.Attachment_Mode = false; }
            else
            {
                if (CB.Attachment_Port_One != null || CB.Attachment_Port_Two != null)
                {
                    CB.Attachment_Mode_Options = EditorGUILayout.Toggle("Attachment Mode Options", CB.Attachment_Mode_Options);
                    if (CB.Attachment_Mode_Options)
                    {
                        CB.Change_Connector_Offset = EditorGUILayout.Toggle("Change Connector Offset", CB.Change_Connector_Offset);
                        if (CB.Change_Connector_Offset)
                        {
                            CB.Has_New_Connector_Offset_Been_Found = true;
                            CB.Connector_Offset = EditorGUILayout.Vector3Field("Connector Offset", CB.Connector_Offset);
                            if (!Reset_Connector_Offset)
                            {
                                CB.gameObject.transform.position = CB.Attachment_Port_One.gameObject.transform.position;
                                CB.Connector_Offset = Vector3.zero;
                                Reset_Connector_Offset = true;
                            }
                            CB.gameObject.transform.position = CB.Attachment_Port_One.gameObject.transform.position + CB.Connector_Offset;
                            if (GUILayout.Button("Save New Connector Offset"))
                            {
                                CB.Change_Connector_Offset = false;
                                CB.Has_New_Connector_Offset_Been_Found = false;
                                Reset_Connector_Offset = false;
                            }
                        }
                        else { Reset_Connector_Offset = false; }
                    }
                }

                if (CB.Connection_Port_One == null)
                {
                    CB.Attachment_Port_One = EditorGUILayout.ObjectField("Attachment Port One", CB.Attachment_Port_One, typeof(GameObject), true) as GameObject;
                    if (!CB.Has_New_Connector_Offset_Been_Found)
                    {
                        if (CB.Attachment_Port_One != null && CB.Attachment_Port_One.gameObject.GetComponent<Platform_Behavior>() != null)
                        {
                            CB.Attachment_Port_One_PB = CB.Attachment_Port_One.gameObject.GetComponent<Platform_Behavior>();
                            if (CB.Connector_Offset != Vector3.zero || CB.Has_Connector_Offset_Been_Found) { CB.gameObject.transform.position = CB.Attachment_Port_One.gameObject.transform.position + CB.Connector_Offset; }
                            if (!CB.Has_Connector_Offset_Been_Found)
                            {
                                CB.gameObject.transform.position = CB.Attachment_Port_One.gameObject.transform.position;
                                CB.gameObject.transform.rotation = CB.Attachment_Port_One.gameObject.transform.rotation;
                                CB.Has_Connector_Offset_Been_Found = true;
                            }
                        }
                    }
                }
            }
        }

        CB.Connection_Mode = EditorGUILayout.Toggle("Connection Mode (Platform(s) connects to connector)", CB.Connection_Mode);
        if (!CB.Connection_Mode)
        {
            if (CB.Connection_Port_One != null) { CB.CPO_List_Index = 0; DestroyImmediate(CB.Connection_Port_One, true); }
            if(CB.Connection_Port_Two != null) { CB.CPT_List_Index = 0; DestroyImmediate(CB.Connection_Port_Two, true); }
        }
        else
        {
            CB.PLB = GameObject.FindGameObjectWithTag("Scene Manager").GetComponent<Prefab_Loader_Behavior>();
            if (CB.PLB != null)
            {
                EditorGUILayout.LabelField("Connector Miscellaneous");
                if (GUILayout.Button("Rotate Connector Forty-Five Degrees Left"))
                {
                    CB.gameObject.transform.Rotate(0f, -45f, 0f);
                    if(CB.Connection_Port_One != null) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_One, "Left"); }
                    if(CB.Connection_Port_Two != null) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_Two, "Left"); }
                }
                if (GUILayout.Button("Rotate Connector Forty-Five Degrees Right"))
                {
                    CB.gameObject.transform.Rotate(0f, 45f, 0f);
                    if (CB.Connection_Port_One != null) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_One, "Right"); }
                    if (CB.Connection_Port_Two != null) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_Two, "Right"); }
                }

                EditorGUILayout.LabelField("Connector Ports");

                // CB.Connection_Port_One = EditorGUILayout.ObjectField("Connection Port One:", CB.Connection_Port_One, typeof(GameObject), true) as GameObject;
                if (CB.Attachment_Port_One == null)
                {
                    GUIContent Connection_Port_One = new GUIContent("Connection Port One");
                    CB.CPO_List_Index = EditorGUILayout.Popup(Connection_Port_One, CB.CPO_List_Index, CB.PLB.Prefab_String_Array);
                }

                bool Check_For_Connector_One_Change = CB.Check_For_Change(CB.CPO_List_Index, CB.COPI);
                if (Check_For_Connector_One_Change)
                {
                    CB.COPI = CB.CPO_List_Index;
                    if (CB.CPO_List_Index != 0)
                    {
                        CB.Instantiate_Prefab("Connection Port One");
                        CB.Attachment_Port_One_PB = CB.Connection_Port_One.AddComponent<Platform_Behavior>();
                        CB.Attachment_Port_One_PB.CB_Target = CB.Attachment_Port_One_PB.Set_CB_Target(CB.gameObject);
                    }
                    else if (CB.Connection_Port_One != null && CB.CPO_List_Index == 0)
                    {
                        DestroyImmediate(CB.Connection_Port_One, true);
                        CB.Connector_One_Translation_Offset = Vector3.zero;
                    }
                }

                if (CB.Connection_Port_One != null)
                {
                    CB.Connector_One_Center_Point = CB.gameObject.transform.GetChild(0).GetComponent<Transform>();
                    if (CB.Connector_One_Translation_Offset == Vector3.zero) { CB.Connection_Port_One.transform.position = CB.Connector_One_Center_Point.position; }
                    else { CB.Connection_Port_One.transform.position = CB.Connector_One_Center_Point.position + CB.Connector_One_Translation_Offset; }
                    // CB.Connection_Port_One.transform.rotation = Connector_One_Center_Point.rotation;

                    CB.Enable_Port_One_Miscellaneous_Options = EditorGUILayout.Toggle("Port One Miscellaneous Settings", CB.Enable_Port_One_Miscellaneous_Options);
                    if (CB.Enable_Port_One_Miscellaneous_Options) { if(GUILayout.Button("Change Center Point"))
                        {
                            CB.Connection_Port_One.transform.position = CB.Change_Transform_Point(CB.COCP_Index, "Connector Port One", CB.Connection_Port_One.transform.position);
                            CB.COCP_Index += 1;
                            if (CB.COCP_Index < -1){ CB.COCP_Index = 0; } 
                            else if (CB.COCP_Index > CB.Connection_Port_One.transform.Find("Platform Connectors").transform.childCount - 1) { CB.COCP_Index = 0; }
                        }
                    }

                    CB.Enable_Port_One_Transform_Options = EditorGUILayout.Toggle("Port One Transform Settings", CB.Enable_Port_One_Transform_Options);
                    if (CB.Enable_Port_One_Transform_Options)
                    {
                        // Rotate
                        if (GUILayout.Button("Rotate Forty-Five Degrees Left")) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_One, "Left"); }
                        else if (GUILayout.Button("Rotate Forty-Five Degrees Right")) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_One, "Right"); }

                        // Translate
                        CB.Connector_One_Translation_Offset = EditorGUILayout.Vector3Field("Translation Offset: ", CB.Connector_One_Translation_Offset);
                        // CB.Object_Translation(CB.Connection_Port_One, CB.Connector_One_Translation_Offset);
                    }
                }

                // CB.Connection_Port_Two = EditorGUILayout.ObjectField("Connection Port Two:", CB.Connection_Port_Two, typeof(GameObject), true) as GameObject;
                if (CB.Attachment_Port_Two == null)
                {
                    GUIContent Connection_Port_Two = new GUIContent("Connection Port Two");
                    CB.CPT_List_Index = EditorGUILayout.Popup(Connection_Port_Two, CB.CPT_List_Index, CB.PLB.Prefab_String_Array);
                }

                bool Check_For_Connector_Two_Change = CB.Check_For_Change(CB.CPT_List_Index, CB.CTPI);
                if (Check_For_Connector_Two_Change)
                {
                    CB.CTPI = CB.CPT_List_Index;
                    if (CB.CPT_List_Index != 0)
                    {
                        CB.Instantiate_Prefab("Connection Port Two");
                        CB.Attachment_Port_Two_PB = CB.Connection_Port_Two.AddComponent<Platform_Behavior>();
                        CB.Attachment_Port_Two_PB.CB_Target = CB.Attachment_Port_Two_PB.Set_CB_Target(CB.gameObject);
                    }
                    else if (CB.Connection_Port_Two != null && CB.CPT_List_Index == 0)
                    {
                        DestroyImmediate(CB.Connection_Port_Two, true);
                        CB.Connector_Two_Translation_Offset = Vector3.zero;
                    }
                }

                if (CB.Connection_Port_Two != null)
                {
                    CB.Connector_Two_Center_Point = CB.gameObject.transform.GetChild(1).GetComponent<Transform>();
                    if (CB.Connector_Two_Translation_Offset == Vector3.zero) { CB.Connection_Port_Two.transform.position = CB.Connector_Two_Center_Point.position; }
                    else { CB.Connection_Port_Two.transform.position = CB.Connector_Two_Center_Point.position + CB.Connector_Two_Translation_Offset; }
                    // CB.Connection_Port_Two.transform.rotation = Connector_Two_Center_Point.rotation;

                    CB.Enable_Port_Two_Transform_Options = EditorGUILayout.Toggle("Port Two Transform Settings", CB.Enable_Port_Two_Transform_Options);
                    if (CB.Enable_Port_Two_Transform_Options)
                    {
                        // Rotate
                        if (GUILayout.Button("Rotate Forty-Five Degrees Left")) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_Two, "Left"); }
                        else if (GUILayout.Button("Rotate Forty-Five Degrees Right")) { CB.Object_Rotation_Left_Or_Right(CB.Connection_Port_Two, "Right"); }

                        // Translate
                        CB.Connector_Two_Translation_Offset = EditorGUILayout.Vector3Field("Translation Offset: ", CB.Connector_Two_Translation_Offset);
                        // CB.Object_Translation(CB.Connection_Port_Two, CB.Connector_Two_Translation_Offset);
                    }
                }
            }
        }
    }
}
#endif