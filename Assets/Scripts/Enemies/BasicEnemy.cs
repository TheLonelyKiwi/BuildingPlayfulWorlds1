using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//Summary: basic enemy which chases player or aggros on turrets when attacked. 
//Daan Meijneken, 18/12/2023, BPW1
public class BasicEnemy : MonoBehaviour
{
    //components
    private NavMeshAgent agent;
    
    //variables 
    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform hitPoint;
    
    //internal variables
    private int currentHealth;
    private Vector2[] roamPoints = new Vector2[4]; 
    private Vector3 currentRoamPoint;
    private float shotDelay = 0f;
    
    //state machine
    private bool hasBeenHitToggle = false;
    private GameObject currentTarget;
    private GameObject hitTarget; 
    private enum states
    {
        Roaming,
        Chasing,
        Attacking,
        Dead
    }
    private states currentState;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shotDelay = stats.attackDelay;
        agent.stoppingDistance = stats.attackRange - 0.2f;

        currentHealth = stats.health; 
        //sets new roam points
        for (int i = 0; i < roamPoints.Length; i++)
        {
            roamPoints[i] = SetNewRoamPoint(transform.position, stats.detectionRange);
        }

        currentRoamPoint = roamPoints[0];
    }
    
    void Update()
    {
        CheckForStateChange();
    }

    private void UpdateAgent(Vector3 destination, float speed) //handles speed and rotation of agent
    {
        agent.speed = speed;
        agent.destination = destination;
        
        Vector3 moveDirection = new Vector3(agent.velocity.x, agent.velocity.y, 0f);
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        }
    }

    private void CheckForStateChange()
    {
        switch (currentState)
        {
            case states.Roaming: Roaming(); break;
            case states.Chasing: Chasing(); break;
            case states.Attacking: Attacking(); break;
            case states.Dead: IsDead(); break;
        }
    }
    
    private void Roaming()
    {
        if (stats.priority != EnemyStats.AttackPriority.Static)
        {
            //moves between points in radius around spawn
            var nextPoint = roamPoints[Random.Range(0, roamPoints.Length)];
            if (Vector2.Distance(transform.position, currentRoamPoint) < 0.2f || !agent.hasPath)
            {
                currentRoamPoint = nextPoint;
            } else if (agent.velocity.magnitude < 0.1f) //check if agent is stuck and set new roam point
            {
                currentRoamPoint = nextPoint;
            }

            UpdateAgent(currentRoamPoint, stats.roamSpeed);
            
            //checks for target depending on targeting priority
            switch (stats.priority)
            {
                case EnemyStats.AttackPriority.None:
                    
                    //checks if either turret (closest) or player is in range, if both are in range it picks one at random. 
                    List<GameObject> targets = new List<GameObject>();
                    
                    if (CheckForTarget("Player"))
                    {
                        targets.Add(currentTarget);
                    }
                    if (CheckForTarget("Turret"))
                    {
                        targets.Add(currentTarget);
                    }
                    
                    if (targets.Count > 0)
                    {
                        currentTarget = targets[Random.Range(0, targets.Count)];
                        currentState = states.Chasing;
                    }
                    targets.Clear();
                    break;
                case EnemyStats.AttackPriority.OnHit:
                    if (hasBeenHitToggle)
                    {
                        currentTarget = hitTarget;
                        currentState = states.Chasing;
                        
                        hasBeenHitToggle = false;
                    }
                    break;
                case EnemyStats.AttackPriority.Turret:
                    if (CheckForTarget("Turret"))
                    {
                        currentState = states.Chasing;
                    }
                    break;
                case EnemyStats.AttackPriority.Player: 
                    if (CheckForTarget("Player"))
                    {
                        currentState = states.Chasing;
                    }
                    break;
            }
        }
    }
    
    private void Chasing()
    {
        if (currentTarget != null)
        {
            Vector3 destination = currentTarget.transform.position;
            float distance = CheckIfInRange(destination);
        
            if (distance >= stats.detectionRange)
            {
                currentState = states.Roaming;
                currentTarget = null;
                return;
            } else if (distance <= stats.attackRange)
            {
                currentState = states.Attacking;
                return;
            }

            UpdateAgent(destination, stats.chaseSpeed);
        }
        else
        {
            currentState = states.Roaming;
        }
    }
    
    private void Attacking()
    {
        if (currentTarget == null ||
            Vector2.Distance(currentTarget.transform.position, transform.position) >= stats.attackRange)
        {
            currentState = states.Roaming;
            return;
        }

        shotDelay += Time.deltaTime;
        if (shotDelay >= 1 / stats.attackDelay)
        {
            MakeAttack();
            shotDelay = 0f;
        }
    }
    
    private Vector2 SetNewRoamPoint(Vector2 currentPos, float range)
    {
        return currentPos + Random.insideUnitCircle * range;
    }

    private bool CheckForTarget(string targetTag)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, stats.detectionRange, transform.position, 0f);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag(targetTag))
                {
                    currentTarget = hit.transform.gameObject;
                    return true;
                }
            }
        }
        return false;
    }
    
    private float CheckIfInRange(Vector3 target)
    {
        return Vector2.Distance(transform.position, target);
    }

    private void MakeAttack()
    {
        if (stats.attackRange < 1.5)
        {
            try
            { 
                GameObject meleeAttack = Instantiate(stats.projectile, hitPoint.position, quaternion.identity);
                meleeAttack.GetComponent<MeleeAttack>().SetDamage(stats.attackDamage);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            } 
        }
        else
        {
            try
            {
                GameObject projectile = Instantiate(stats.projectile, hitPoint.position, quaternion.identity);
                projectile.GetComponent<Bullet>().SetStats(stats.attackDamage, gameObject);
                projectile.GetComponent<Rigidbody2D>().AddForce((transform.up * -1) * 15, ForceMode2D.Impulse);
            }
            catch
            { 
                Debug.LogError("Please attach a projectile to " + stats.name + " or lower range to =< 1.5!");
            }
        }
        
    }
    private void IsDead()
    {
        Instantiate(stats.drop, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        Destroy(gameObject);
    }
    public void TakeDamage(int damage, GameObject attacker)
    {
        currentHealth -= damage;
        hitTarget = attacker;
        hasBeenHitToggle = true;
        
        gameObject.AddComponent<DamageFlash>();
        
        if (currentHealth <= 0)
        {
            currentState = states.Dead;
        }
    }
    
    //Gizmo for editor debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stats.detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }
}