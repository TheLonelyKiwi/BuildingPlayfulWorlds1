using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TurretHeatlh : MonoBehaviour
{
    [SerializeField] private GameObject turretSprite;
    [SerializeField] private TurretStats turretStats;
    [SerializeField] private GameObject scrapDrop;

    private int currenthealth;

    private void Awake()
    {
        currenthealth = turretStats.health;
    }
    
    private void KillTheTurret()
    {
        Instantiate(scrapDrop, transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void RestoreHealth()
    {
        currenthealth = turretStats.health;
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        turretSprite.AddComponent<DamageFlash>();
        
        if (currenthealth <= 0)
        {
            KillTheTurret();
        }
    }
}
