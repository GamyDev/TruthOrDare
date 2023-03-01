using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModeWindow : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        Invoke("DisableAnimator", 1.8f);
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
    }
}
