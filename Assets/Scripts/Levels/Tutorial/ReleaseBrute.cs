using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseBrute : AbstractDoor
{

    public override void OpenDoor()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("IsOpen", true);
    }
}
