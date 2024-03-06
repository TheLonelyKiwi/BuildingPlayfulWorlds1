using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: handles turret statemachine and turret functions such as rotation and firing. 
//Daan Meijneken, 19/11/2023, BPW1

public class TurretController : MonoBehaviour
{
    //turret components
    [SerializeField] private Transform firePoint;
    
    //turret stats
    [SerializeField] private TurretStats stats;
    
    //layers for targeting
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private LayerMask wallLayer;
    
    //state vars 
    public int currentAmmo { get; private set; }
    public bool hasTarget = false;
    public bool hasBeenPickedUp = false;

    public Vector3 initialRotationPoint;
    
    private GameObject currentTarget; 
    
    //turret state variables
    public TurretStateMachine StateMachine;
    
    public TurretRoamState RoamState;
    public TurretFiringState FiringState;
    public TurretNoAmmoState NoAmmoState;
    public TurretStaticState StaticState;

    private void Awake()
    {
        StateMachine = new TurretStateMachine();
        
        RoamState = new TurretRoamState(this, StateMachine);
        FiringState = new TurretFiringState(this, StateMachine);
        NoAmmoState = new TurretNoAmmoState(this, StateMachine);
        StaticState = new TurretStaticState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.StartMachine(StaticState);
        
        currentAmmo = stats.maxAmmo;
    }

    private void Update()
    {
        if (hasTarget && !CheckTargetIsInRange())
        {
            hasTarget = false;
            currentTarget = null;
        }
        
        if (!hasTarget && !hasBeenPickedUp)
        {
            SetNewTarget();
            return;
        } 
        
        StateMachine.currentState.UpdateState();
        
    }
    
    //Functions for picking and dropping turret. 
    public void InitializeTurret()
    {
        StateMachine.ChangeState(RoamState);
        SetStartingRotationPoint();
    }
    
    public void PickupTurret()
    {
        StateMachine.ChangeState(StaticState);
        hasBeenPickedUp = true;
        hasTarget = false;
        currentTarget = null; 
    }

    public void PlaceTurret()
    {
        if (FindObjectOfType<ScrapManager>().RefillAmmo() && currentAmmo < stats.maxAmmo) //refills ammo if player has sufficient scrap
        {
            currentAmmo = stats.maxAmmo;
            GetComponent<TurretHeatlh>().RestoreHealth();
        }
        
        StateMachine.ChangeState(RoamState);
        hasTarget = false;
        hasBeenPickedUp = false;
    }

    public void RotateTurret(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
         
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * stats.rotationSpeed);
    }

    public void FireTurret()
    {
        GameObject bullet = Instantiate(stats.bulletInstance, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetStats(stats.bulletDamage, gameObject);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * stats.bulletVelocity, ForceMode2D.Impulse);
        
        currentAmmo--;
    }

    private void SetNewTarget()
    {
       RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, stats.detectionRange, transform.position, 0f, enemiesLayer );

       if (hits.Length > 0)
       {
           currentTarget = hits[0].collider.gameObject;
           hasTarget = true;
       }
    }

    private bool CheckTargetIsInRange()
    {
        if (currentTarget == null)
        {
            return false;
        }
        else
        {
            if (Vector2.Distance(currentTarget.transform.position, transform.position) <= stats.detectionRange)
            {  
               return true;
            } else {return false;}
        }
    }

    private void SetStartingRotationPoint()
    {
        initialRotationPoint = transform.position + Vector3.up * 3;
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }
    
    public float GetFireRate()
    {
        return stats.fireRate;
    }

    //Gizmo for editing debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.detectionRange);
    }
}
