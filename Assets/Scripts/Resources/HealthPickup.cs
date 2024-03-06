using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: heals player on contact if they are not at max health.
//Daan Meijneken, 20/12/2020, BPW1
public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 3;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager i))
            {
                if (i.GetCurrentHealth < i.GetMaxHealth)
                {
                    i.HealPlayer(healAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}
