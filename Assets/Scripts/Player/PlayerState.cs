using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    
    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void UpdateState() {}
    
    public PlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
}
