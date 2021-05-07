using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    public enum lampMode {Mixed, Baked};

    public lampMode Mode;
    public GameObject light, lampEmissive, lampNotEmissive;

    void Awake()
    {
        GameManagerScript.eventChanged += ChangeEvent;
    }

    protected void ChangeEvent() 
    {
        if (Mode == lampMode.Mixed) SetLampMode(GameManagerScript.activeEvent.actions.lightsMixed);
        else if (Mode == lampMode.Baked) SetLampMode(GameManagerScript.activeEvent.actions.lightsBaked);
    }

    void SetLampMode(bool on)
    {
        light.SetActive(on);
        lampEmissive.SetActive(on);
        lampNotEmissive.SetActive(!on);
    }
}
