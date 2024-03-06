using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNoAmmoState : TurretState
{
    public TurretNoAmmoState(TurretController turret, TurretStateMachine stateMachine) : base(turret, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        Debug.Log("Entered no ammo state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        turret.RotateTurret(turret.initialRotationPoint);

        if (turret.currentAmmo > 0)
        {
            stateMachine.ChangeState(turret.RoamState);
        }
    }
}
