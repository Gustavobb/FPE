using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Animator animator;

    public void PlayAnim(string anim)
    {
        animator.SetTrigger(anim);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
