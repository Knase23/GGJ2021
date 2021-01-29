using System;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.AI;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TriggerAiTalk : MonoBehaviour
{
    public AIDialogue talker;
    private CircleCollider2D Collider2D;
    
    private void Start()
    {
        talker ??= GetComponent<AIDialogue>();
        Collider2D = GetComponent<CircleCollider2D>();
        Collider2D.radius = talker.talkRange - 2;
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
