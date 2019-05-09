using Ming.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateB : FSMState
{
    private float _timer;

    public override void OnEnter(params object[] keys)
    {
        string tip = string.Empty;

        if (keys != null && keys.Length > 0)
        {
            tip = keys[0].ToString();
        }

        Debug.LogFormat("Enter State B {0}", tip);
    }

    public override void OnExit()
    {
        Debug.Log("Exit State B ");
    }

    public override void OnStay()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1)
        {
            _timer = 0;

            Debug.Log("Stay B");
        }
    }
}
