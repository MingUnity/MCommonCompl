using Ming.EventHub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        Handler_01 h1 = new Handler_01();

        Handler_02 h2 = new Handler_02();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Log"))
        {
            MEventHub.Instance.Dispatch((int)EventId.DebugLog, MEventArgs.Empty);
        }

        if (GUILayout.Button("Error"))
        {
            MEventHub.Instance.Dispatch((int)EventId.DebugLogError, MEventArgs.Empty);
        }

        if (GUILayout.Button("Warning"))
        {
            MEventHub.Instance.Dispatch((int)EventId.DebugLogWarning, new WarningArgs()
            {
                content = "警告 警告!"
            });
        }
    }
}

public enum EventId
{
    DebugLog = 0,

    DebugLogError = 1,

    DebugLogWarning = 2
}
