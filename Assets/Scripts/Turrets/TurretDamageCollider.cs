using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//summary: just a stupid solution regarding colliders 
//Daan Meijneken, 20/12/2023, BPW1
public class TurretDamageCollider : MonoBehaviour
{
    [SerializeField] private TurretHeatlh turretHealth;

    public void TakeDamage(int damage)
    {
        turretHealth.TakeDamage(damage);
    }
}
