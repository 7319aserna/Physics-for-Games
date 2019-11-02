using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    Animator anim;
    Controller player;
    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Controller>();
    }
    public void PrintEvent()
    {
        player.canJump = true;
        print("can jump");
    }
}
