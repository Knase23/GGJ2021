using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit : MonoBehaviour
{
    public ParticleSystem sys;
    public int amount;

    private void Start()
    {
        if (amount == 0) amount = 10;
    }

    public void Emit30()
    {
        sys.Emit(amount);
    }
}