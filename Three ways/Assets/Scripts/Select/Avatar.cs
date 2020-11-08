using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetSet()
    {
        animator.SetBool("idle", false );
    }
    public void SetIdle()
    {
        animator.SetBool("idle", true );
    }
}
