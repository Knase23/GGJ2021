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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enabled == false) return;
        
        if (other.CompareTag("Player"))
        {
            talker.Talk();
        }
    }
}
