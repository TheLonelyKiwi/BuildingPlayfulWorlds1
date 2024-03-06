using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    
    public void LoadScene(string name)
    {
        StartCoroutine(FadeOut(name));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    private IEnumerator FadeOut(string name)
    {
        yield return FindObjectOfType<ScreenFade>().Fade(ScreenFade.FadeDirection.In);
        SceneManager.LoadScene(name);
    }
}
