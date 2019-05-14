using Ming.FSM;
using UnityEngine;

public class Main : MonoBehaviour
{
    private IFSMSystem _fsmSystem;

    private void Start()
    {
        _fsmSystem = new FSMSystem();

        IFSMState stateA = new StateA(_fsmSystem);

        IFSMState stateB = new StateB();

        IFSMState stateC = new StateC(_fsmSystem);

        stateA[(int)State.B] = stateB;

        stateA[(int)State.C] = stateC;

        stateB[(int)State.C] = stateC;

        stateC[(int)State.A] = stateA;

        stateC[(int)State.B] = stateB;

        _fsmSystem.AddState(stateA, true);

        _fsmSystem.AddState(stateB);

        _fsmSystem.AddState(stateC);
    }

    private void Update()
    {
        if (_fsmSystem != null)
        {
            _fsmSystem.Update();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_fsmSystem != null)
            {
                _fsmSystem.SetTransition((int)State.C);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_fsmSystem != null)
            {
                _fsmSystem.TurnDefault();
            }
        }
    }
}

public enum State
{
    A = 0,
    B = 1,
    C = 2
}
