using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CellPhoneScript : MonoBehaviour
{
    Animator animator;
    Image m_Image;
    bool active = false;
    float timer = 0;
    public Text clock;

    public Sprite message1, message2, message3;
    public static DateTime tme;
    
    public AudioSource beep;

    void Awake()
    {
        GameManagerScript.eventChanged += ChangeEvent;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        m_Image = GetComponent<Image>();
        m_Image.sprite = message1;
        tme = new DateTime(2020, 4, 5, 11, 56, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            active = !active;
            beep.Play();
            animator.SetBool("Up", active);
        }

        timer += Time.deltaTime;

        if (timer >= 60) 
        {
            tme = tme.AddMinutes(1);
            timer = 0f;
        }
        
        if (tme.Minute == 58 && tme.Hour == 11 && timer == 0f) 
        {
            m_Image.sprite = message2;
            GetComponent<AudioSource>().Play();
        }
        else if (tme.Minute == 00 && tme.Hour == 12 && timer == 0f) 
        {
            m_Image.sprite = message3;   
            GetComponent<AudioSource>().Play(); 
        }

        clock.text = tme.ToString("t");
    }

    void ChangeEvent() 
    {
        if (GameManagerScript.activeEvent.activePawns.cellphone) GameManagerScript.nextEventTrigger += TriggerCond;
    }

    void TriggerCond() { if (active) GameManagerScript.nextEventTrigger -= TriggerCond; }
}
