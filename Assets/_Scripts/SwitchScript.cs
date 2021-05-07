using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : Interactable
{
    public Animator animator;
    public override void Interact()
    {
        hasInteracted = true;
        animator.SetTrigger("Press");
    }

    protected override void ChangeEvent() 
    {
        hasInteracted = false;
        canInteract = false;
        for (int i = 0; i < GameManagerScript.activeEvent.activePawns.switchs.Length; i++)
            if (GameManagerScript.activeEvent.activePawns.switchs[i] == id) 
            {
                canInteract = true;
                GameManagerScript.nextEventTrigger += TriggerCond;
            }
    }

    protected override void TriggerCond() 
    {
        if (hasInteracted) GameManagerScript.nextEventTrigger -= TriggerCond;
    }
}
