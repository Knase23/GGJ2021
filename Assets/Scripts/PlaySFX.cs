using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class PlaySFX : MonoBehaviour
{
  private AudioSource source; 
  public List<AudioClip> clips;

  private void Start()
  {
    source = GetComponent<AudioSource>();
  }

  public void Play()
  {
    source.PlayOneShot(clips[Random.Range(0,clips.Count)]);
  }
}
