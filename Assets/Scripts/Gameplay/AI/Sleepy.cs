using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleepy : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator ??= GetComponent<Animator>();
    }

    public void SetAwake(bool state)
    {
        animator.SetBool("Awake",state);
    }
}
