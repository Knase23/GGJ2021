using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class ShipAndMonster : MonoBehaviour
{
    public GameObject holdingParent;
    public AString titleText;
    public GameObject releasedParent;

    // Start is called before the first frame update
    void Start()
    {
        holdingParent.SetActive(true);
        releasedParent.SetActive(false);
        Invoke(nameof(ReleaseAndWinGame), 4f);
    }

    public void ReleaseAndWinGame()
    {
        StartCoroutine(DoReleaseAndWinGame());
    }

    private IEnumerator DoReleaseAndWinGame()
    {
        Fade fade = FindObjectOfType<Fade>();
        fade.FadeIn();
        fade.DoWinMusic();
        FindObjectOfType<ShowLearnedWords>().gameObject.SetActive(false);
        titleText.text = "\"The kind creature released my rocket\"";
        yield return new WaitForSeconds(Fade.FadeInTime + 0.25f);
        holdingParent.SetActive(false);
        releasedParent.SetActive(true);
       
        fade.FadeOut();
        yield return new WaitForSeconds(Fade.FadeOutTime + 2.25f);
        fade.FadeIn();
        yield return new WaitForSeconds(Fade.FadeInTime + 0.25f);
        fade.DoWinScreen();
        titleText.text = "\"I hope I can come back and meet my new friends someday\"";
        
        yield return new WaitForSeconds(6f);
        titleText.text = "\"Heroglyph\" - A GGJ2021 game";
        
        yield return new WaitForSeconds(6.5f);
        titleText.text = "Art: Max Friberg";
        
        yield return new WaitForSeconds(5.5f);
        titleText.text = "Programming: Jesper Uddefors";
        
        yield return new WaitForSeconds(5.5f);
        titleText.text = "Music: AIVA.com";
        
        yield return new WaitForSeconds(5.5f);
        titleText.text = "SFX: transformed from the public Celeste FMOD project";

        yield return new WaitForSeconds(6.5f);
        titleText.text = "Thank you for playing!";
        
        FindObjectOfType<CloseGame>().ShowMe();
        yield break;
    }
}