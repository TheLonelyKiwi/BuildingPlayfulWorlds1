using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFiringState : TurretState
{

    private float shotDelay = 0f;
    private float fireRate = 1f;
    public TurretFiringState(TurretController turret, TurretStateMachine stateMachine) : base(turret, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        fireRate = turret.GetFireRate();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!turret.hasTarget)
        {
            stateMachine.ChangeState(turret.RoamState);
            return;
        } else if (turret.currentAmmo <= 0)
        {
            stateMachine.ChangeState(turret.NoAmmoState);
            return;
        }

        if (CanShoot())
        {
            turret.FireTurret();
            shotDelay = 0f;
        }
        
        turret.RotateTurret(turret.GetCurrentTarget().transform.position);
    }

    private bool CanShoot()
    {
        shotDelay += Time.deltaTime;

        if (shotDelay >= 1f / fireRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
