using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType {
    CastSpell
}

public abstract class CustomEvent
{
    //Base class for ingame events (Non-unity related)
    public abstract void ProcessEvent();
    public abstract void PauseEvent();

    //Most events update UI in some way
    //public abstract void updateUI(List<UIElementType> uiElements = null);
}
