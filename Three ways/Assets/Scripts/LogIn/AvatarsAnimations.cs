using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarsAnimations : MonoBehaviour
{
    private Animator animator;  
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StopHit()
    {
        animator.SetBool("hit", false);
    }
    public void Hit()
    {
        animator.SetBool("hit", true);
    }
    public void Die()
    {
        animator.SetBool("die", true);
    }
    public void StopDie()
    {
        animator.SetBool("die", false);
    }
}
