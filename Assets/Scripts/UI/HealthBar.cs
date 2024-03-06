using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    private PlayerHealthManager healthManager;
    private List<HealthHeart> hearts = new List<HealthHeart>();
    
    private void Start()
    {
        healthManager = FindObjectOfType<PlayerHealthManager>();
        Draw();
    }

    private void OnEnable()
    {
        PlayerHealthManager.OnPlayerDamaged += Draw;
    }

    private void OnDisable()
    {
        PlayerHealthManager.OnPlayerDamaged -= Draw;
    }

    private void Draw()
    {
        DestroyAllHearts();
        
        int remainder = healthManager.GetMaxHealth % 2;
        int heartsToDraw = healthManager.GetMaxHealth / 2 + remainder;
        for (int i = 0; i < heartsToDraw; i++)
        {
            CreateHeart();
        }

        //fills hearts based on player health
        for (int j = 0; j < hearts.Count; j++)
        {
            int newStatus = Mathf.Clamp(healthManager.GetCurrentHealth - (j * 2), 0, 2);
            hearts[j].ChangeImage((HealthHeart.HeartStatus)newStatus);
        }
    }

    private void CreateHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        
        HealthHeart component = newHeart.GetComponent<HealthHeart>();
        component.ChangeImage(HealthHeart.HeartStatus.Empty);
        hearts.Add(component);
    }

    private void DestroyAllHearts()
    {
        foreach (Transform i in transform)
        {
            Destroy(i.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
