using Ming.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateC : FSMState
{
    private IFSMSystem _fsmSystem;

    public StateC(IFSMSystem fsmSystem)
    {
        this._fsmSystem = fsmSystem;
    }

    public override void OnEnter(params object[] keys)
    {
        Debug.Log("Enter State C");
    }

    public override void OnExit()
    {
        Debug.Log("Exit State C");
    }

    public override void OnStay()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_fsmSystem != null)
            {
                _fsmSystem.SetTransition((int)State.B, "C to B");
            }
        }
    }
}
