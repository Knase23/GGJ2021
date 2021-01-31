using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image curtain;
    public Image winScreen;
    private const float FadeInTime = 1.5f;
    private const float FadeOutTime = 2.5f;
    public AudioClip winMusic;
    public AudioSource source;

    private Coroutine fadeRoutine;

    public void Start()
    {
        winScreen.color = Color.white;
        winScreen.canvasRenderer.SetAlpha(0);
        curtain.color = Color.black;
        SetToOpaque();
        Invoke(nameof(FadeOut), 0.4f);
        // Invoke(nameof(DoWinScreen), 0.4f);
    }

    public void DoFadeInThenOut()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(Fading());
    }

    public void SetToTransparent()
    {
        curtain.canvasRenderer.SetAlpha(0);
    }

    public void SetToOpaque()
    {
        curtain.canvasRenderer.SetAlpha(1);
    }

    public void FadeIn()
    {
        curtain.CrossFadeAlpha(1, FadeInTime, true);
    }

    public void FadeOut()
    {
        curtain.CrossFadeAlpha(0, FadeOutTime, true);
    }

    private IEnumerator Fading()
    {
        SetToTransparent();
        yield return null;
        FadeIn();
        yield return new WaitForSeconds(2.6f);
        FadeOut();
    }

    public void DoWinScreen()
    {
        Ambience ambience = FindObjectOfType<Ambience>();
        if (ambience != null)
        {
            ambience.FadeOut();
        }

        source.PlayOneShot(winMusic);
        SetToTransparent();
        winScreen.CrossFadeAlpha(1, 4, true);
    }
}