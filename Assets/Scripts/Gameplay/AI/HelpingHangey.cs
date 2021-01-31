using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HelpingHangey : MonoBehaviour
{

    private Fade _fade;

    public float fadeInTime = 0.5f;
    public float blackDuration = 0.5f;
    public float fadeOutTime = 0.5f;
    public float showTime = 0.75f;

    public UnityEvent onStartBlackHelp = new UnityEvent();
    public UnityEvent onComplete = new UnityEvent();
    public UnityEvent onHelpSleepyComplete = new UnityEvent();

    public GameObject playerHelpSprite;
    public GameObject sleepyHelpSprite;
    
    
    private Coroutine _coroutine = null;
    private void Start()
    {
        _fade = FindObjectOfType<Fade>();
        playerHelpSprite.SetActive(false);
        sleepyHelpSprite.SetActive(false);
    }

    public void HelpFromPlayer()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(PlayerHelp());

    }

    IEnumerator PlayerHelp()
    {
        _fade.FadeIn(fadeInTime);
        yield return new WaitForSecondsRealtime(fadeInTime);
        onStartBlackHelp?.Invoke();
        playerHelpSprite.SetActive(true);
        yield return new WaitForSecondsRealtime(blackDuration);
        _fade.FadeOut(fadeOutTime);
        yield return new WaitForSecondsRealtime(fadeOutTime);
        yield return new WaitForSecondsRealtime(showTime);
        
        _fade.FadeIn(fadeInTime);
        yield return new WaitForSecondsRealtime(fadeInTime);
        playerHelpSprite.SetActive(false);
        
        onComplete?.Invoke();
        yield return new WaitForSecondsRealtime(blackDuration);
        _fade.FadeOut(fadeOutTime);
        
        yield return new WaitForSecondsRealtime(fadeOutTime);
        yield break;
        
    }
    
    IEnumerator SleepyHelp()
    {
        _fade.FadeIn(fadeInTime);
        yield return new WaitForSecondsRealtime(fadeInTime);
        onStartBlackHelp?.Invoke();
        sleepyHelpSprite.SetActive(true);
        yield return new WaitForSecondsRealtime(blackDuration);
        _fade.FadeOut(fadeOutTime);
        yield return new WaitForSecondsRealtime(fadeOutTime);
        yield return new WaitForSecondsRealtime(showTime);
        
        _fade.FadeIn(fadeInTime);
        yield return new WaitForSecondsRealtime(fadeInTime);
        sleepyHelpSprite.SetActive(false);
        onComplete?.Invoke();
        onHelpSleepyComplete?.Invoke();
        yield return new WaitForSecondsRealtime(blackDuration);
        _fade.FadeOut(fadeOutTime);
        gameObject.SetActive(false);
        yield break;
        
    }
    
    
    
    
    public void HelpWithSleepy()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(SleepyHelp());
    }
    
    
}
