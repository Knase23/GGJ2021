using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class TitleTriggerBox : MonoBehaviour
{
    public string message;

    public float autoRemoveTime = 10f;

    public AString title;

    private bool triggered;

    public List<TitleTriggerBox> newActivatedTriggerBoxes;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            title.text = message;
            Invoke(nameof(AutoRemove), autoRemoveTime);
        }
    }

    private void AutoRemove()
    {
        if (title.text == message)
            title.text = string.Empty;
    }
}