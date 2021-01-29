using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    public UnityEvent _OnTriggerEnter = new UnityEvent();
    public UnityEvent _OnTriggerExit = new UnityEvent();


    private void OnTriggerEnter2D(Collider2D other)
    {
        _OnTriggerEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _OnTriggerExit?.Invoke();
    }
}
