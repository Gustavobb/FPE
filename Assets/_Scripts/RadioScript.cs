using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : Interactable
{
    public GameObject music, sequeak;

    public override void Interact() 
    {
        base.Interact();
        hasInteracted = !hasInteracted;

        if (music.activeSelf)
            music.SetActive(!music.activeSelf);

        if (sequeak.activeSelf)
            sequeak.SetActive(!sequeak.activeSelf);
    }

    protected override void ChangeEvent() 
    {
        canInteract = false;
        hasInteracted = false;
        if (GameManagerScript.activeEvent.activePawns.radio) 
        {
            canInteract = true;
            GameManagerScript.nextEventTrigger += TriggerCond;
        }

        if (GameManagerScript.activeEvent.actions.radioSound == "Squeak") sequeak.SetActive(true);
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
