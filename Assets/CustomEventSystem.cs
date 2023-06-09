using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    //TODO:: There might be a need to create multiple threads depending on the size of the event queue,
    //Which will require synchronization of event order
    Queue<CustomEvent> eventQueue = new Queue<CustomEvent>();
    //TODO:: implement an array for all ongoing events
    //And pause their coroutines

    void OnUpdate()
    {
        PollEvents();
    }

    void PollEvents()
    {
        if (eventQueue.Count > 0)
            eventQueue.Dequeue().ProcessEvent();
    }

    public void SendEvent(CustomEvent e)
    {
        eventQueue.Enqueue(e);
    }
}
