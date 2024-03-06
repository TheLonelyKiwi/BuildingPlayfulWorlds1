using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPickup : MonoBehaviour
{
    [SerializeField] private string identifier;
    [SerializeField] private int bonusScrap = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<ScrapManager>(out ScrapManager i))
            {
                if (i.GetScrapAmount < i.GetMaxScrap)
                {
                    i.ModifyScrap(identifier, bonusScrap);
                    Destroy(gameObject);
                }
            }
        }
    }
}
