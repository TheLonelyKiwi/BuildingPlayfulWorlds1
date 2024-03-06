using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    private TextMeshProUGUI hpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.text = "HP:" + GetComponent<PlayerHealthManager>().GetCurrentHealth;
    }
}
