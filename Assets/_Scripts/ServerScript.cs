using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : Interactable
{
    public GameObject windowsOn, windowsOff;

    public override void Interact() 
    {
        base.Interact();
        hasInteracted = !hasInteracted;
        windowsOff.SetActive(!windowsOff.activeSelf);
        windowsOn.SetActive(!windowsOn.activeSelf);
    } 

    protected override void ChangeEvent() 
    {
        canInteract = false;
        if (GameManagerScript.activeEvent.activePawns.server) 
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
        }
    }
}
