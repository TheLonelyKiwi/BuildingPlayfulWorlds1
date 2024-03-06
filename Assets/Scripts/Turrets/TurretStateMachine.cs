using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine
{
    
    public TurretState currentState;

    public void StartMachine(TurretState startingState)
    {
        currentState = startingState;
        startingState.EnterState();
    }
    
    public void ChangeState(TurretState newState)
    {
        currentState.ExitState();
        currentState = newState;
        newState.EnterState();
    }
}
