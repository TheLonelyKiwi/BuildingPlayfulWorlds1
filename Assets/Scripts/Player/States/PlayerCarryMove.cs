using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryMove : PlayerState
{
    private float moveSpeed = 2.5f;
    public PlayerCarryMove(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    
    public override void UpdateState()
    {
        base.UpdateState();

        if (player.IsCarrying)
        {
            if (!player.IsMoving)
            {
                stateMachine.ChangeState(player.carryIdleState);
            }
            else
            {
                player.MovePlayer(player.MovementInput, moveSpeed);
            }
        }
        else
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
