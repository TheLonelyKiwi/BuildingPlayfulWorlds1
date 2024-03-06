using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float hitboxrange;
    private int damage;
    
    public void SetDamage(int damage)
    {
        this.damage = damage;
        exectuteAttack();
    }

    private void exectuteAttack()
    {
        //play attack animation
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitboxrange);
        
        foreach(Collider2D hit in hits)
        {
            switch (hit.tag)
            {
                case "Turret":
                    hit.GetComponent<TurretDamageCollider>().TakeDamage(damage);
                    break;
                case "Player":
                    hit.GetComponent<PlayerHealthManager>().TakeDamage(damage);
                    break;
            }
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitboxrange);
    }
}
