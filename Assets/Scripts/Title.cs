using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class Title : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    public AString aString;

    private Coroutine textDisplayRoutine;

    public string prevString;

    private void Start()
    {
        titleText = GetComponent<TextMeshProUGUI>();
        titleText.text = String.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (prevString != aString.text)
        {
            prevString = aString.text;
            if (textDisplayRoutine != null)
            {
                StopCoroutine(textDisplayRoutine);
            }

            textDisplayRoutine = StartCoroutine(ShowText());
        }
    }

    public IEnumerator ShowText()
    {
        string builtString = string.Empty;

        do
        {
            var i = titleText.text.Length - 1;
            if (i >= 0 && titleText.text.Length > i) titleText.text = titleText.text.Remove(i);
            yield return new WaitForSeconds(0.035f);
        } while (titleText.text.Length > 0);

        foreach (var t in aString.text)
        {
            builtString += t;
            titleText.text = builtString;
            yield return new WaitForSeconds(0.06f);
        }
    }
}