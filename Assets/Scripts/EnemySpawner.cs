using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//summary: spawns enemies around the player and increases difficulty over time
//Daan Meijneken, 22/11/2023, BPW1
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float SpawnRange = 14f;
    [SerializeField] private float innerSpawnRangeMod = 0.6f;


    [System.Serializable]
    struct EnemyType
    {
        public GameObject enemy;
        public int spawnChance;
    }

    [SerializeField] private EnemyType[] enemies;
    [SerializeField] private int initialSpawnCap = 10;
    [SerializeField] private int maxSpawnCap = 9;
    [SerializeField] private float capIncreaseRate = 15f;

    private int spawnCap;
    private float timetoIncreaseCap = 0f;
    private List<GameObject> enemiesSpawned = new List<GameObject>();

    private void Awake()
    {
        spawnCap = initialSpawnCap;
    }

    private void Update()
    {
        UpdateTimer();
        
        if (enemiesSpawned.Count < spawnCap)
        {
            SpawnEnemy(GetRandomEnemy());
        }

        CheckForTheDead();
    }

    private void UpdateTimer() //increases difficulty over time.
    {
        if (timetoIncreaseCap < capIncreaseRate)
        {
            timetoIncreaseCap += Time.deltaTime;
        }
        else
        {
            timetoIncreaseCap = 0f;
            IncreaseSpawnCap();
        }
    }

    private void IncreaseSpawnCap()
    {
        if (spawnCap < maxSpawnCap)
        {
            spawnCap++;
        }
        else
        {
            spawnCap = maxSpawnCap;
        }
    }
    private void SpawnEnemy(GameObject enemy) //spawns an enemy on top of nav mesh within range of the player
    {
        Vector2 spawnPos = GetRandomSpawnPos((Vector2)playerPos.position, innerSpawnRangeMod, SpawnRange);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(new Vector3(spawnPos.x, spawnPos.y, 0f), out hit, 1f, NavMesh.AllAreas))
        {
            GameObject enemySpawned = Instantiate(enemy, hit.position, Quaternion.identity);
            enemiesSpawned.Add(enemySpawned);
        }
    }

    private void CheckForTheDead() //removes dead enemies from the list
    {
        for (int i = enemiesSpawned.Count; i-- > 0;)
        {
            if (enemiesSpawned[i] == null)
            {
                enemiesSpawned.RemoveAt(i);
            }
            else
            {
                float dist = (enemiesSpawned[i].transform.position - playerPos.position).sqrMagnitude;
                if (dist > SpawnRange * SpawnRange)
                {
                    Destroy(enemiesSpawned[i]);
                    enemiesSpawned.RemoveAt(i);
                }
            }
        }
    }

private GameObject GetRandomEnemy() //code used from Stackoverflow https://gamedev.stackexchange.com/questions/153840/how-can-i-spawn-items-based-on-probabilities 
    {
        int totalweight = enemies.Sum(t => t.spawnChance);
        int value = Random.Range(0, totalweight);
        
        GameObject objToSpawn = null;
        foreach (EnemyType enemy in enemies)
        {
            if (value < enemy.spawnChance)
            {
                objToSpawn = enemy.enemy;
                break;
            }
            value -= enemy.spawnChance;
        }
        
        return objToSpawn;
    }

private Vector2 GetRandomSpawnPos(Vector2 pos, float minDistance, float range) //makes sure enemies don't spawn too close to the player
    {
        Vector2 spawnPosMin = pos + Random.insideUnitCircle * (range * minDistance);
        Vector2 spawnPosMax = pos + Random.insideUnitCircle * range;
        return spawnPosMax + Random.Range(0, 1) * (spawnPosMax - spawnPosMin);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(playerPos.position, SpawnRange * innerSpawnRangeMod);
        Gizmos.DrawWireSphere(playerPos.position, SpawnRange);
        
    }
}
