using Ming.EventHub;
using UnityEngine;

public class Handler_02 : IEventListener
{
    public void HandleEvent(int eventId, IEventArgs args)
    {
        Debug.LogFormat("i am 02  get event {0}", eventId);

        switch (eventId)
        {
            case EventId.DebugLogWarning:
                {
                    if (args != null)
                    {
                        Debug.LogWarningFormat("i am 02 ,get msg : {0}", args.GetCValue<string>());
                    }
                }

                break;
        }
    }
}
