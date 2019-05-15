using Ming.EventHub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler_01 : IEventListener
{
    public Handler_01()
    {
        MEventHub.Instance.AddListener(0, this);

        MEventHub.Instance.AddListener(1, this);

        MEventHub.Instance.AddListener(2, this);
    }

    public void HandleEvent(int eventId, IEventArgs args)
    {
        EventId id = (EventId)eventId;

        switch (id)
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
