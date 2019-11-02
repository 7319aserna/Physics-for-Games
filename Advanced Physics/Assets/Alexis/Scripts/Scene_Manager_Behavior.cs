using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager_Behavior : MonoBehaviour
{
    private TextMeshProUGUI t_TMPro;

    [HideInInspector]
    public bool can_Game_Be_Restarted = true;

    public Image parent;

    public List<GameObject> user_Interface_List = new List<GameObject>();

    [HideInInspector]
    public int t_Counter = 0;

    void Start() { enable_Heads_Up_Display(0, user_Interface_List); t_TMPro = parent.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); }
    private void Update() { if (can_Game_Be_Restarted && Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(0); } }

    public void enable_Heads_Up_Display(int _selected_Index, List<GameObject> _selected_List)
    {
        for(int SJ = 0; SJ < _selected_List.Count; SJ++)
        {
            if(_selected_Index == SJ) { _selected_List[SJ].SetActive(true); }
            else { _selected_List[SJ].SetActive(false); }
        }
    }

    public void update_Counter()
    {
        t_Counter += 1;
        t_TMPro.text = t_Counter.ToString();
    }
}