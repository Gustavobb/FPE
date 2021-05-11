using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    public Animator animator;
    public override void Interact()
    {
        base.Interact();
        hasInteracted = !hasInteracted;
        animator.SetBool("Open", hasInteracted);
    }

    protected override void ChangeEvent() 
    {
        canInteract = false;
        for (int i = 0; i < GameManagerScript.activeEvent.activePawns.doors.Length; i++)
            if (GameManagerScript.activeEvent.activePawns.doors[i] == id) 
            {
                canInteract = true;
            }
    }
}
