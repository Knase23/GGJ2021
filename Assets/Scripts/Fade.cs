using System;
using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image curtain;
    public Image winScreen;
    public Image introGraphic;
    public const float FadeInTime = 1.5f;
    public const float FadeOutTime = 2.5f;
    public AudioClip winMusic;
    public AudioSource source;

    private Coroutine fadeRoutine;

    public void Awake()
    {
        introGraphic.color = Color.white;
        introGraphic.canvasRenderer.SetAlpha(1);
        winScreen.color = Color.white;
        winScreen.canvasRenderer.SetAlpha(0);
        curtain.color = Color.black;
        SetToOpaque();
       
        Invoke(nameof( RemoveIntroGraphic), 1.5f + 4.5f);

        Invoke(nameof(FadeOut), 3.4f + 4.5f);
        // Invoke(nameof(DoWinScreen), 0.4f);
    }

    private void RemoveIntroGraphic()
    {
        introGraphic.CrossFadeAlpha(0,3.3f,true);
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

    public void FadeIn(float duration)
    {
        curtain.CrossFadeAlpha(1, duration, true);
    }

    public void FadeOut(float duration)
    {
        curtain.CrossFadeAlpha(0, duration, true);
    }

    private IEnumerator Fading()
    {
        SetToTransparent();
        yield return null;
        FadeIn();
        yield return new WaitForSeconds(2.6f);
        FadeOut();
    }

    public void DoWinMusic()
    {
        Ambience ambience = FindObjectOfType<Ambience>();
        if (ambience != null)
        {
            ambience.FadeOut();
        }

        source.PlayOneShot(winMusic);
    }

    public void DoWinScreen()
    {
        foreach (var movementScript in FindObjectsOfType<Movement>())
        {
            movementScript.enabled = false;
        }


        // SetToTransparent();
        winScreen.CrossFadeAlpha(1, 6, true);
    }
}