using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//old system
//Daan Meijneken 20/11/2023, BPW1

public class TurretRadiusCheck : MonoBehaviour
{

    [SerializeField] public float detectionRadius = 10f;
    private bool isDisabled;
    
    private ITurretInRangeTrigger tTrigger;
    private TurretController turret;
    
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        tTrigger = new ITurretInRangeTrigger();
        turret = GetComponentInParent<TurretController>();
        
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = detectionRadius;
    }

    private void Update()
    {
        if (turret.hasBeenPickedUp)
        {
            if (isDisabled)
            {
                return;
            }
            else
            {
                isDisabled = true; 
                tTrigger.EnemiesInRange.Clear();
            }
        }
        else if (isDisabled)
        {
            isDisabled = false; 
            FindNewObjects();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isDisabled)
        {
            tTrigger.AddEnemyInRange(other.gameObject);
        }
    }
    
    public void RemoveEnemyFromList(GameObject enemy)
    {
        tTrigger.RemoveEnemyInRange(enemy);
    }

    public GameObject FindNewTarget()
    {
        if (tTrigger.EnemiesInRange != null)
        {
            return FindClosestTarget(tTrigger.EnemiesInRange);
        }
        else
        {
            return null;
        }
    }
    private void FindNewObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D i in colliders)
        {
            if (i.gameObject.tag == "Enemy")
            {
                tTrigger.AddEnemyInRange(i.gameObject); 
            }
        }
    }
    private GameObject FindClosestTarget(List<GameObject> targets) //finds closest target in list, code partly from Unity forms.
    {
        //set current pos early in case this takes too long and the turret moves. 
        Vector3 currentPos = transform.position;
        
        GameObject closestTarget = null;
        float ClosestDistance = Mathf.Infinity;
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 directionToTarget = targets[i].transform.position - currentPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < ClosestDistance)
            {
                ClosestDistance = dSqrToTarget;
                closestTarget = targets[i];
            }
        }

        return closestTarget;
    }
    
}
