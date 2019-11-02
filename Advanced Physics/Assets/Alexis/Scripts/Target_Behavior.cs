using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Target_Behavior : MonoBehaviour
{
    private bool Has_Collision_Occured = false;
    private bool Has_Target_Reached_Mark = false;
    private Controller Player_Controller;
    private Sling_Behavior SB;

    // Element 0 = Outer Ring, Element 1 = First Ring, Element 2 = Second Ring, Element 3 = Third Ring, Element 4 = Center Ring
    public List<GameObject> Rings = new List<GameObject>();
    public TextMeshProUGUI Score_Text;

    void Start()
    {
        for (int SJ = 0; SJ < Rings.Count; SJ++) { Rings[SJ].AddComponent<Collision_Check_Behavior>(); }
        Player_Controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        SB = GameObject.Find("Slingshot").GetComponent<Sling_Behavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Has_Target_Reached_Mark)
        {
            for (int SJ = 0; SJ < Rings.Count; SJ++)
            {
                Has_Collision_Occured = Rings[SJ].GetComponent<Collision_Check_Behavior>().Has_Collision_Occured;
                if (Has_Collision_Occured)
                {
                    // Outer Ring
                    if (Rings[SJ].name == "Outer Ring") { Score_Text.text = "1"; }
                    // First Ring
                    if (Rings[SJ].name == "First Ring") { Score_Text.text = "5"; }
                    // Second Ring
                    if (Rings[SJ].name == "Second Ring") { Score_Text.text = "10"; }
                    // Third Ring
                    if (Rings[SJ].name == "Third Ring") { Score_Text.text = "25"; }
                    // Center Ring
                    if (Rings[SJ].name == "Center Ring") { Score_Text.text = "50"; }

                    Has_Target_Reached_Mark = true;
                }
            }
        }
        if (Has_Target_Reached_Mark) { Player_Controller.die(); SB.Has_Player_Been_Flung = false; }
    }
}
