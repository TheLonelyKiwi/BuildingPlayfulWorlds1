using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public static event Action OnPlayerDamaged;

    [SerializeField] private Transform playerSprite;
    [SerializeField] private int maxHealth = 6;
    private int currentHealth;
    public int GetMaxHealth { 
        get { return maxHealth; }
    }
    public int GetCurrentHealth { 
        get { return currentHealth; }
    }

    private void Awake()
    {
        currentHealth = GetMaxHealth;
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnPlayerDamaged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerSprite.gameObject.AddComponent<DamageFlash>();
        OnPlayerDamaged?.Invoke();
        
        if (currentHealth <= 0)
        {
            KillThePlayer();
        }
    }

    private void KillThePlayer()
    {
        GetComponent<PlayerController>().SetIsDead();
        FindObjectOfType<SceneLoader>().LoadScene("LoseScreen");
    }
}
