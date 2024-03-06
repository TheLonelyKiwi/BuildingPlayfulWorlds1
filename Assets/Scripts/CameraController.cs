using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

//Summary: moves the camera to the player's position and applies a rough camera boundary.
//Daan Meijneken, 19/12/2023, BPW1
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Vector2 minPos, maxPos;
    private void Update()
    {
        if (transform.position != player.position)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10);
            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = targetPos;
        }
    }
}
