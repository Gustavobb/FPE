using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseScript : Interactable
{
    public override void Interact()
    {
        base.Interact();
        hasInteracted = true;
    }

    protected override void ChangeEvent() 
    {
        canInteract = false;
        if (GameManagerScript.activeEvent.activePawns.fuseBox) 
        {
            canInteract = true;
            GameManagerScript.nextEventTrigger += TriggerCond;
        }
    }

    protected override void TriggerCond() 
    {
        if (hasInteracted) 
        {
            GameManagerScript.nextEventTrigger -= TriggerCond;
            canInteract = false;
        }
    }
}
