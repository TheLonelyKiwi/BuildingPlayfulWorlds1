using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: When an object gets damaged this script will be added that gives the object a red hue for a short time. 
//Daan Meijneken + SasquatchBStudios on reddit https://www.reddit.com/r/Unity2D/comments/ikfv9c/how_to_make_player_flash_red_while_taking_damage/ for idea of changing hue color.


public class DamageFlash : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        Destroy(this);
    }
}
