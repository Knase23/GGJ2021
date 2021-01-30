using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class RockHolder : MonoBehaviour
{
    public List<SpriteRenderer> RockObjects;

    public List<Sprite> RockSprites;

   

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        SwapRockSprites();
        ResizeRocks();
        FlipRocks();
        PushRocksBackOrForward();
        HideRocks();
        
    }

    private void PushRocksBackOrForward()
    {
        foreach (SpriteRenderer rockObject in RockObjects)
        {
            rockObject.sortingOrder = Random.value > 0.5f ? 4 : -4;
        }
    }


    private void FlipRocks()
    {
        foreach (SpriteRenderer rockObject in RockObjects)
        {
            rockObject.flipX = Random.value > 0.5f;
        }
    }

    private void ResizeRocks()
    {
        foreach (var spriteRenderer in RockObjects)
        {
            spriteRenderer.gameObject.transform.localScale = Vector3.one + Vector3.one * Random.Range(-0.45f, 0.1f);
        }
    }

    private void HideRocks()
    {
        foreach (var spriteRenderer in RockObjects)
        {
            spriteRenderer.enabled = (Random.value < 0.8f);
        }
    }

    public void SwapRockSprites()
    {
        foreach (var rockObject in RockObjects)
        {
            rockObject.sprite = RockSprites[Random.Range(0, RockSprites.Count)];
        }
    }


}