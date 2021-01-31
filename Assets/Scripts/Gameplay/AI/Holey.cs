using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holey : MonoBehaviour
{

    public Transform target;
    public Vector3 originalPosition;

    public float transitionTime = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    public void GoDown()
    {
        gameObject.LeanMove(target.position, transitionTime);
    }

    public void GoUp()
    {
        gameObject.LeanMove(originalPosition, transitionTime);
    }
    
    
}
