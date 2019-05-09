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

        stateA[State.B] = stateB;

        stateA[State.C] = stateC;

        stateB[State.C] = stateC;

        stateC[State.A] = stateA;

        stateC[State.B] = stateB;

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
                _fsmSystem.SetTransition(State.C);
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
    A,
    B,
    C
}
