using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    public UnityEvent _OnTriggerEnter = new UnityEvent();
    public UnityEvent _OnTriggerExit = new UnityEvent();
    
    public string compareTag;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(compareTag))
            _OnTriggerEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(compareTag))
            _OnTriggerExit?.Invoke();
    }
}