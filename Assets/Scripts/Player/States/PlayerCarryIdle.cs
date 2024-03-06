using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryIdle : PlayerState
{
    public PlayerCarryIdle(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (player.IsCarrying)
        {
            if (player.IsMoving)
            {
                stateMachine.ChangeState(player.carryMoveState);
            }
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
