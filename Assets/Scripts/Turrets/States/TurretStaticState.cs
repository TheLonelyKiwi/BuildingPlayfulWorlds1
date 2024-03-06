using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStaticState : TurretState
{
    public TurretStaticState(TurretController turret, TurretStateMachine stateMachine) : base(turret, stateMachine)
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
