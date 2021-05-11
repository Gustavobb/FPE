using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int id;
    public bool canInteract = false;
    protected bool hasInteracted = false;
    
    void Awake()
    {
        GameManagerScript.eventChanged += ChangeEvent;
    }

    public virtual void Interact() { GetComponent<AudioSource>().Play(); }
    public virtual void DoneInteract() { hasInteracted = false; }
    protected virtual void ChangeEvent() {}
    protected virtual void TriggerCond() {}
}
