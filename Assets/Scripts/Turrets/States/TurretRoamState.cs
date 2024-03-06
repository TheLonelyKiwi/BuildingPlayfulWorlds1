using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRoamState : TurretState
{
    private float randomDistance = 10f;
    private Vector3 currentTarget;
    public TurretRoamState(TurretController turret, TurretStateMachine stateMachine) : base(turret, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        currentTarget = GetNewRandomPoint(GetNewRandomPoint(turret.transform.position));
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (turret.hasTarget)
        {
            stateMachine.ChangeState(turret.FiringState);
            return;
        }
        
        //moves turret between different points. 
        turret.RotateTurret(new Vector3(10, 10, 0));
    }
    
    private Vector3 GetNewRandomPoint(Vector3 center)
    {
        float angle = Random.Range(0, 2f * Mathf.PI);
        return center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * randomDistance;
    }
}
