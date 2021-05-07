using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Events
{
    public int id;
    public string eventTask;
    public string nextEventTriggerType;
    public int nextEvent;
    public Actions actions;
    public ActivePawns activePawns;
}
