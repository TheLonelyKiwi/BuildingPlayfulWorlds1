using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    public PlayerState currentState;
    
    public void StartMachine(PlayerState startingState)
    {
        currentState = startingState; 
        startingState.EnterState();
    }
    
    public void ChangeState(PlayerState newState)
    {
        currentState.ExitState();
        currentState = newState;
        newState.EnterState();
    }
}
    