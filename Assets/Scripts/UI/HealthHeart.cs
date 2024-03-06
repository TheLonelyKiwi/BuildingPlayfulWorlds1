using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//summary: displays correct heart sprite based on player health. 
//source: used code from BMo, https://www.youtube.com/watch?v=5NViMw-ALAo 

public class HealthHeart : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart, halfHeart, emptyHeart;
    private Image heartImage;
    
    public enum HeartStatus
    {
        Full = 2,
        Half = 1,
        Empty = 0
    }

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void ChangeImage(HeartStatus state)
    {
        switch (state)
        {
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
            case HeartStatus.Half: 
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Empty: 
                heartImage.sprite = emptyHeart;
                break;
        }
    }
}
