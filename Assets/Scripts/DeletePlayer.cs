using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePlayer : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private Animator animator;
    [SerializeField] private Scrollbar scrollbar;


    public void InputClose()
    {
        verticalLayoutGroup.enabled = false;
        animator.enabled = true;
        scrollbar.value += 0.1428f;
        animator.Play("DeletePlayer");
        Invoke("DeleteObject", 0.5f);
    }


    void DeleteObject()
    {
       verticalLayoutGroup.enabled = true;
        Destroy(gameObject);
    }


}
