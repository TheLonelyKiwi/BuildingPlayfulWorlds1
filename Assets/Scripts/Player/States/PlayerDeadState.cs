using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState 
{
    public PlayerDeadState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
