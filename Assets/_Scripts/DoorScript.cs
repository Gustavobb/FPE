using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    public Animator animator;

    public override void Interact()
    {
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
                GameManagerScript.nextEventTrigger += TriggerCond;
                Debug.Log(id);
            }
    }

    protected override void TriggerCond() 
    {
        if (hasInteracted) GameManagerScript.nextEventTrigger -= TriggerCond;
    }
}
