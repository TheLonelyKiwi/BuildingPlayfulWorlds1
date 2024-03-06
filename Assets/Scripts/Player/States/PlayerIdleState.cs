using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!player.IsCarrying)
        {
            if (player.IsMoving)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
        else
        {
            stateMachine.ChangeState(player.carryIdleState);
        }
    }
}
