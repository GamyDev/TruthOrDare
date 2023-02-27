using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void InputClose()
    {
        GameManager.instance.players.verticalLayoutGroup.enabled = false;
        animator.enabled = true;
        GameManager.instance.players.scrollbar.value += 0.1428f;
        animator.Play("DeletePlayer");
        Invoke("DeleteObject", 0.5f);
        GameManager.instance.players.RemovePlayer(transform.GetSiblingIndex());
    }


    void DeleteObject()
    {
        GameManager.instance.players.verticalLayoutGroup.enabled = true;
        Destroy(gameObject);
     
    }


}
