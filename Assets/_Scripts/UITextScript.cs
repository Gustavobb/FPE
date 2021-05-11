using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour
{
    public enum storyMode {Tutorial, Narrative};
    public storyMode Mode;

    Animator animator;

    void Awake()
    {
        GameManagerScript.eventChanged += ChangeEvent;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void ChangeEvent() 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TextANim")) AnimEvent();
        animator.SetBool("Change", true);
    }

    void Narrative()
    {
        GetComponent<UnityEngine.UI.Text>().text = GameManagerScript.activeEvent.eventTask;
    }

    void Tutorial()
    {
        GetComponent<UnityEngine.UI.Text>().text = GameManagerScript.activeEvent.tutorialText;
    }

    void AnimEvent()
    {
        if (Mode == storyMode.Narrative) Narrative();
        if (Mode == storyMode.Tutorial) Tutorial();
    }

    void Done()
    {
        animator.SetBool("Change", false);
    }
}
