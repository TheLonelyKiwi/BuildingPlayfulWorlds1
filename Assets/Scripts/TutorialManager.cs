using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Summary: opens and closes a tutorial box
//Daan Meijneken, 21/11/2023, BPW1
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup tutorialBox;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private float fadeTime = 0.4f;

    private bool isFaded = false;


    public void TriggerDisplayOn(string text)
    {
        tutorialText.text = text;
        Fade();
    }

    public void TriggerDisplayText(string text)
    {
        tutorialText.text = text;
    }
    
    public void TriggerDisplayOff()
    {
        Fade();
    }
    private void Fade()
    {
        StartCoroutine(FadeBox(tutorialBox, tutorialBox.alpha, isFaded ? 0 : 1));
        
        isFaded = !isFaded;
    }

    public IEnumerator FadeBox(CanvasGroup canvas, float start, float end)
    {
        float c = 0f;
        
        while (c < fadeTime)
        {
            c += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(start, end, c / fadeTime);
            yield return null;
        }
    }
}
