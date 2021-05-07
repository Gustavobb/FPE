using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static Events activeEvent;
    public TextAsset jsonFile;
    EventsData eventDatajson;
    int eventCounter = 0;
    LightmapData[] lightmap_data;
    
    public delegate void EventChanged();
    public static EventChanged eventChanged;

    public delegate void NextEventTrigger();
    public static NextEventTrigger nextEventTrigger;

    void Awake()
    {
        lightmap_data = LightmapSettings.lightmaps;
        eventDatajson = JsonUtility.FromJson<EventsData>(jsonFile.text);
    }

    void Start()
    {
        activeEvent = eventDatajson.events[eventCounter];
        eventChanged();
    }

    void Update()
    {
        if (nextEventTrigger == null) ChangeActiveEvent();
        else nextEventTrigger();
    }

    void TurnLightMapsOff()
    {
        LightmapSettings.lightmaps = new LightmapData[]{};
    }

    void TurnLightMapsOn()
    {
        LightmapSettings.lightmaps = lightmap_data;
    }

    void ChangeActiveEvent()
    {
        eventCounter ++;
        if (eventCounter == eventDatajson.events.Length) return;

        activeEvent = eventDatajson.events[eventCounter];
        eventChanged();

        if (activeEvent.actions.lightsBaked) TurnLightMapsOn();
        else if (!activeEvent.actions.lightsBaked) TurnLightMapsOff();
    }
}
