using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpeechSFX : MonoBehaviour
{
    public List<AudioClip> voiceClips;

    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Speak(int min = 3, int max = 6)
    {
        StartCoroutine(Speaking(min, max));
    }

    private IEnumerator Speaking(int min, int max)
    {
        AudioClip prevClip = null;
        AudioClip c = null;


        for (int i = 0; i < Random.Range(min, max); i++)
        {
            if (i > 0 && max > 1)
            {
                do
                {
                    c = voiceClips[Random.Range(0, voiceClips.Count)];
                } while (c == prevClip);
            }
            else
            {
                c = voiceClips[Random.Range(0, voiceClips.Count)];
            }

            _source.PlayOneShot(c);
            while (_source.isPlaying)
            {
                yield return new WaitForFixedUpdate();
            }

            prevClip = c;
            yield return new WaitForSeconds(0.1f / (_source.pitch / 2));
        }
    }
}