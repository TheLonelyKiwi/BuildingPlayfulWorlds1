using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
    
    protected TurretController turret;
    protected TurretStateMachine stateMachine;
    
    public virtual void EnterState() {} 
    public virtual void ExitState() {}
    public virtual void UpdateState() {}
    
    public TurretState(TurretController turret, TurretStateMachine stateMachine)
    {
        this.turret = turret;
        this.stateMachine = stateMachine;
    }
}
