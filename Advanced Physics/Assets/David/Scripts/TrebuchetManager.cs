using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetManager : MonoBehaviour
{
    private Custom_Trebuchet_Manager CTM;


    [HideInInspector]
    public bool has_Trebuchet_Been_Launched = false;

    TrebuchetRelease[] trebs;
    // Start is called before the first frame update
    void Start()
    {
        CTM = GameObject.Find("Trebuchet").GetComponent<Custom_Trebuchet_Manager>();
        trebs = GetComponentsInChildren<TrebuchetRelease>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CTM.is_Player_Within_Boundaries)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                has_Trebuchet_Been_Launched = true;
                StartCoroutine("Launch");
            }
        }
    }

    IEnumerator Launch()
    {
        for (int i = 0; i < trebs.Length; i++)
        {
            trebs[i].Fire();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
