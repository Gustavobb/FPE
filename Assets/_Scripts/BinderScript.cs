using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinderScript : Interactable
{
    public GameObject child;
    public override void Interact() 
    {
        base.Interact();
        hasInteracted = !hasInteracted;
    } 

    protected override void ChangeEvent() 
    {
        canInteract = false;
        if (GameManagerScript.activeEvent.activePawns.binder) 
        {
            canInteract = true;
            GameManagerScript.nextEventTrigger += TriggerCond;
        }
    }

    protected override void TriggerCond()
    {
        if (hasInteracted) 
        {
            canInteract = false;
            GameManagerScript.nextEventTrigger -= TriggerCond;
            child.SetActive(false);
        }
    }
}
