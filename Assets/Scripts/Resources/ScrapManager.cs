using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//summary: manages scrap resource and pickups. 
//Daan Meijneken, 15/12/2023, BPW1
public class ScrapManager : MonoBehaviour
{
    [SerializeField] private ScrapBar uiBar;
    
    //scrap modifiers
    [SerializeField] private List<string> keyWords;
    [SerializeField] private List<int> scrapModifiers;
    private Dictionary<string, int> scrapCosts;
    
    //scrap variables
    private int maxScrap = 100;
    private int scrapAmount = 0;
    
    public int GetMaxScrap { get { return maxScrap; } }
    public int GetScrapAmount { get { return scrapAmount; } }

    private void Awake()
    {
        scrapAmount = 100;
        uiBar.SetValue(scrapAmount);
        
        //converts arrays to dictionary
        if (keyWords.Count == scrapModifiers.Count)
        {
            scrapCosts = new Dictionary<string, int>();
            for (int i = 0; i < keyWords.Count; i++)
            {
                scrapCosts.Add(keyWords[i], scrapModifiers[i]);
            }
        }
        else
        {
            Debug.LogError("ScrapManager: keyWords and scrapModifiers are not the same size!");
        }
    }
    
    public void ModifyScrap(string identifier, int bonus) //modifies total scrap, gets called externally.
    {
        scrapAmount += scrapCosts[identifier] + bonus;
        if (scrapAmount >= maxScrap)
        {
            scrapAmount = maxScrap;
        }
        uiBar.SetValue(scrapAmount);
    }

    public bool CanAffordTurret()
    {
        if (scrapAmount == maxScrap)
        {
            scrapAmount -= maxScrap;
            uiBar.SetValue(scrapAmount);
            return true;
        }
        else
        {
            //display gui message
            return false;
        }
    }

    public bool RefillAmmo()
    {
        if (scrapAmount >= 25)
        {
            scrapAmount-= 25;
            uiBar.SetValue(scrapAmount);
            return true;
        } else return false;
    }
}
