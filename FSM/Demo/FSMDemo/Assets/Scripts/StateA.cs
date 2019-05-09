using Ming.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateA : FSMState
{
    private IFSMSystem _fsmSystem;

    public StateA(IFSMSystem fsmSystem)
    {
        this._fsmSystem = fsmSystem;
    }

    public override void OnEnter(params object[] keys)
    {
        Debug.Log("Enter State A");
    }

    public override void OnExit()
    {
        Debug.Log("Exit State A");
    }

    public override void OnStay()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_fsmSystem != null)
            {
                _fsmSystem.SetTransition(State.B,"A to B");
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_fsmSystem != null)
            {
                _fsmSystem.SetTransition(State.C);
            }
        }
    }
}
