using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{

    //base stats
    public int health;
    
    //movement variables
    public float roamSpeed; 
    public float chaseSpeed;
    
    //attack variables
    public float detectionRange;
    public int attackDamage; 
    public float attackRange; 
    public float attackDelay;

    //priority variables
    public enum AttackPriority
    {
        None,
        OnHit,
        Turret,
        Player,
        Static
    }
    public AttackPriority priority;
    
    //drops and misc
    public GameObject drop;
    public GameObject projectile;
}
