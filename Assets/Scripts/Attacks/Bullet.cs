using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

//summary: general purpose bullet, can be created from any source and will only damage the opposing faction
//Daan Meijneken, 17/12/2023, BPW1
public class Bullet : MonoBehaviour
{
    private int damage;
    private float lifeTime = 0f;
    private GameObject origin;

    private enum parentType
    {
        Enemy,
        Turret
    };
    private parentType type;
    
    public void SetStats(int damage, GameObject parent)
    {
        this.damage = damage;
        origin = parent;

        switch (parent.tag)
        {
            case "Enemy": type = parentType.Enemy; break;
            case "Turret": type = parentType.Turret; break;
        }
    }
    private void FixedUpdate()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime >= 2f)
        {
            Destroy(gameObject);
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "Enemy":
                if (type == parentType.Turret)
                {
                    collider.GetComponent<BasicEnemy>().TakeDamage(damage, origin);
                    Destroy(gameObject);
                }
                break;
            case "Turret":
                if (type == parentType.Enemy)
                {
                    collider.GetComponent<TurretDamageCollider>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            case "Player":
                if (type == parentType.Enemy)
                {
                    collider.GetComponent<PlayerHealthManager>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                break;
            case "Shield":
                Destroy(gameObject);
                break;
            case "obstacle":
                Destroy(gameObject);
                break;
            case "Scrap":
                break;
        }
    } 
}
