using System;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.AI;
using UnityEngine;

public class TriggerAiTalk : MonoBehaviour
{
    public AIDialogue talker;
    private void Start()
    {
        talker = GetComponent<AIDialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something Entered the Trigger, something with tag: "+ other.tag);
        if (other.CompareTag("Player"))
        {
            talker.Talk();
        }
    }
}
