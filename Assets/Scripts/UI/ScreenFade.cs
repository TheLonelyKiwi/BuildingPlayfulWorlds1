using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//summary: fades the screen to black on scene change, fades back in on scene load.
// 22/11/2023, BPW1
public class ScreenFade : MonoBehaviour
{
    //Code used from: https://gamedevelopertips.com/unity-how-fade-between-scenes/ Wanted to get this done
    [SerializeField] private float fadeTime = 0.6f;
    [SerializeField] private Image image;
    public enum FadeDirection
    {
        In,
        Out
    }

    private void OnEnable()
    {
        StartCoroutine(Fade(FadeDirection.Out));
    }

    public IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;

        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetImageColor(ref alpha, fadeDirection);
                yield return null;
            }
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetImageColor(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }
    private void SetImageColor(ref float alpha, FadeDirection fadeDirection)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        
        alpha += Time.deltaTime * (1.0f / fadeTime) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
}
