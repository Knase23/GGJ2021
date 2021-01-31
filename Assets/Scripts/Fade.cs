using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image curtain;
    private const float FadeInTime = 1.5f;
    private const float FadeOutTime = 2.5f;

    public void Start()
    {
        curtain.color = Color.black;
        SetToOpaque();
        Invoke(nameof(FadeOut), 0.4f);
    }

    public void DoFadeInThenOut()
    {
        StartCoroutine(Fading());
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
}