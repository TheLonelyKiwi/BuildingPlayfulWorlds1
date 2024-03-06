using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: safe reference to turretcontroller child (is less expensive than GetChild().GetComponent<>(); )
//Daan Meijneken, 22/11/2023, BPW1

public class TurretInfo : MonoBehaviour
{
    [SerializeField] private  TurretController controller;
    [SerializeField] private CircleCollider2D hitBox;
    
    public void InitializeTurret()
    {
        controller.InitializeTurret();
        hitBox.GetComponent<SpriteRenderer>().color = Color.white;
        hitBox.enabled = true;
    }

    public void PickupTurret()
    {
        controller.PickupTurret();
        hitBox.enabled = false;
    }

    public void DropTurret()
    {
        controller.PlaceTurret();
        hitBox.enabled = true;
    }

    public void SetColor(Color color)
    {
        hitBox.GetComponent<SpriteRenderer>().color = color;
    }
}
