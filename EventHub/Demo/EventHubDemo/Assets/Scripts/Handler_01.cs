using Ming.EventHub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler_01 : IEventListener
{
    public void HandleEvent(int eventId, IEventArgs args)
    {
        switch (eventId)
        {
            case EventId.DebugLog:
                Debug.Log("i am 01");
                break;
            case EventId.DebugLogError:
                Debug.LogError("i am 01");
                break;
            case EventId.DebugLogWarning:
                Debug.LogWarning("i am 01");
                break;
            default:
                break;
        }
    }
}
