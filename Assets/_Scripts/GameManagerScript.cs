using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static Events activeEvent;
    public TextAsset jsonFile;
    EventsData eventDatajson;

    bool isTimeUp = false;
    LightmapData[] lightmap_data;
    public delegate void EventChanged();
    public static EventChanged eventChanged;

    public delegate void NextEventTrigger();
    public static NextEventTrigger nextEventTrigger;

    public GameObject monster, creepySound, creepyPiano;

    void Awake()
    {
        lightmap_data = LightmapSettings.lightmaps;
        eventDatajson = JsonUtility.FromJson<EventsData>(jsonFile.text);
    }

    void Start()
    {
        activeEvent = eventDatajson.events[0];
        CheckEventTimer();
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
        activeEvent = eventDatajson.events[activeEvent.nextEvent];
        if (activeEvent.id == 14 && CellPhoneScript.tme.Hour == 11) SceneManager.LoadScene(2);
        if (activeEvent.id == 17) SceneManager.LoadScene(3);
        else eventChanged();
        
        Act();
    }

    void Act()
    {
        CheckEventTimer();
        
        // lights
        if (activeEvent.actions.mantainLights) return;
        if (activeEvent.actions.lightsBaked) TurnLightMapsOn();
        else if (!activeEvent.actions.lightsBaked) TurnLightMapsOff();

        // monster
        if (activeEvent.actions.creepAnimation != "?") 
        {
            monster.SetActive(true);
            monster.GetComponent<MonsterScript>().PlayAnim(activeEvent.actions.creepAnimation);
        }

        if (activeEvent.actions.sound == "creepySound") creepySound.SetActive(true);
        if (activeEvent.actions.sound == "creepyPiano") 
        {
            creepyPiano.SetActive(true);
            creepySound.SetActive(false);
        }
    }

    void CheckEventTimer()
    {
        // event
        if (activeEvent.nextEventTriggerType == "Time9") 
        {
            nextEventTrigger += GetTimesUp;
            StartCoroutine(TimeCounter(9f));
        }

        else if (activeEvent.nextEventTriggerType == "Time6") 
        {
            nextEventTrigger += GetTimesUp;
            StartCoroutine(TimeCounter(6f));
        }

        else if (activeEvent.nextEventTriggerType == "Time3") 
        {
            nextEventTrigger += GetTimesUp;
            StartCoroutine(TimeCounter(3f));
        }
    }

    void GetTimesUp() 
    {
        if (isTimeUp) 
        { 
            nextEventTrigger -= GetTimesUp; 
            isTimeUp = false;
        } 
    }

    IEnumerator TimeCounter(float time)
    {
        yield return new WaitForSeconds(time);
        isTimeUp = true;
    }
}
