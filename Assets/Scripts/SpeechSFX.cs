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

    public void Speak()
    {
        if(voiceClips.Count == 0)
            return;
        
        StartCoroutine(Speaking());
    }

    private IEnumerator Speaking()
    {
        AudioClip prevClip = null;
        AudioClip c = null;


        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            if (i > 0)
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