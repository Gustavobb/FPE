using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopScript : Interactable
{
    public GameObject screenOn, screenOff;

    public override void Interact() 
    {
        base.Interact();
        hasInteracted = !hasInteracted;
        screenOff.SetActive(!screenOff.activeSelf);
        screenOn.SetActive(!screenOn.activeSelf);
    } 

    protected override void ChangeEvent() 
    {
        canInteract = false;
        for (int i = 0; i < GameManagerScript.activeEvent.activePawns.pcs.Length; i++)
            if (GameManagerScript.activeEvent.activePawns.pcs[i] == id) 
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
