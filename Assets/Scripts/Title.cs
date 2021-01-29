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

    private void Start()
    {
        titleText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        titleText.text = aString.text;
    }
}