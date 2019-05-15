using Ming.EventHub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler_02 : IEventListener
{
    public Handler_02()
    {
        MEventHub.Instance.AddListener(2, this);
    }

    public void HandleEvent(int eventId, IEventArgs args)
    {
        Debug.LogFormat("i am 02  get event {0}", (EventId)eventId);

        EventId id = (EventId)eventId;

        switch (id)
        {
            case EventId.DebugLogWarning:
                {
                    WarningArgs wargs = args as WarningArgs;

                    if (wargs != null)
                    {
                        Debug.LogWarningFormat("i am 02 ,get msg : {0}", wargs.content);
                    }
                }

                break;
        }
    }
}
