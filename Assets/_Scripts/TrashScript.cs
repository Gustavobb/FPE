using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : Interactable
{
    PlayerController player;
    public LayerMask layerMask;
    bool trashed = false;
    bool trashedLegit = false;
    public float radius = .07f;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        float step =  10.0f * Time.deltaTime;
        if (hasInteracted) transform.position = Vector3.MoveTowards(transform.position, player.holdPoint.transform.position, step);
        else if (!trashed)
        {
            if (!this.GetComponent<Rigidbody>().useGravity) this.GetComponent<Rigidbody>().useGravity = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach (var hitCollider in hitColliders)
                if (!trashed) 
                {
                    trashed = true;
                    StartCoroutine(WaitToDie(2f));
                }
        }
    }

    public override void Interact()
    {
        base.Interact();
        hasInteracted = !hasInteracted;
        this.GetComponent<Rigidbody>().useGravity = !hasInteracted;
    }

    protected override void ChangeEvent() 
    {
        canInteract = false;
        for (int i = 0; i < GameManagerScript.activeEvent.activePawns.trash.Length; i++)
            if (GameManagerScript.activeEvent.activePawns.trash[i] == id) 
            {
                canInteract = true;
                GameManagerScript.nextEventTrigger += TriggerCond;
            }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (hasInteracted && other.collider.tag != "Player") Interact();
    }

    protected override void TriggerCond() 
    {
        if (trashedLegit) GameManagerScript.nextEventTrigger -= TriggerCond;
    }

    IEnumerator WaitToDie(float time)
    {
        yield return new WaitForSeconds(time);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hitColliders.Length > 0 && !hasInteracted) 
        {
            trashedLegit = true;
            Destroy(gameObject);
        }

        trashed = false;
    }
}
