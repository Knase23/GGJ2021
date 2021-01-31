using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    private bool fade;
    public AudioSource source;

    private void Start()
    {
        fade = false;
        source = GetComponent<AudioSource>();
    }

    public void FadeOut()
    {
        fade = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            source.volume = Mathf.Lerp(source.volume, 0, Time.deltaTime / 2.5f);
        }
    }
}
