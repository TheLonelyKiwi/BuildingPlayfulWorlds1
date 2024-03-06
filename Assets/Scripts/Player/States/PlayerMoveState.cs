using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float moveSpeed = 4f;
    public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!player.IsCarrying)
        {
            if (!player.IsMoving)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                player.MovePlayer(player.MovementInput, moveSpeed);
            }
        }
        else
        {
            stateMachine.ChangeState(player.carryMoveState);
        }
    }
}
