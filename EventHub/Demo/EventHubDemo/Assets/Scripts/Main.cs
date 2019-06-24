using Ming.EventHub;
using UnityEngine;

public class Main : MonoBehaviour
{
    private IEventHub _eventHub;

    private void Awake()
    {
        _eventHub = new MEventHub();
    }

    private void Start()
    {
        Handler_01 h1 = new Handler_01();

        _eventHub.AddListener(0, h1);

        _eventHub.AddListener(1, h1);

        _eventHub.AddListener(2, h1);

        _eventHub.AddListener(2, new Handler_02());
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Log"))
        {
            _eventHub.Dispatch(EventId.DebugLog, SimpleEventArgs.Empty);
        }

        if (GUILayout.Button("Error"))
        {
            _eventHub.Dispatch(EventId.DebugLogError, SimpleEventArgs.Empty);
        }

        if (GUILayout.Button("Warning"))
        {
            _eventHub.Dispatch(EventId.DebugLogWarning, new CommonCEventArgs<string>("Warning Warning"));
        }
    }
}

public static class EventId
{
    public const int DebugLog = 0;

    public const int DebugLogError = 1;

    public const int DebugLogWarning = 2;
}
