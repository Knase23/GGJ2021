using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit : MonoBehaviour
{
    public ParticleSystem sys;
    
    public void Emit30()
    {
        sys.Emit(10);
    }
}