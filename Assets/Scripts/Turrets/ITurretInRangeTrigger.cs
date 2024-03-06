using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//outdated
public class ITurretInRangeTrigger
{
    
    public List<GameObject> EnemiesInRange { get; set; }

    public void AddEnemyInRange(GameObject enemy)
    {
        EnemiesInRange.Add(enemy);
    }

    public void RemoveEnemyInRange(GameObject enemy)
    {
        //fix for an issue that might cause enemies to appear multiple times in array 
        while (EnemiesInRange.Contains(enemy))
        {
            EnemiesInRange.Remove(enemy);
        }
    }
}
