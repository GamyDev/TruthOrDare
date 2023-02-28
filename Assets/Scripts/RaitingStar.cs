using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaitingStar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public static event Action<int> selectedStar;
    public GameObject activeStar;
    public GameObject disableStar;

    public void OnPointerClick(PointerEventData eventData)
    {
        selectedStar?.Invoke(transform.GetSiblingIndex());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedStar?.Invoke(transform.GetSiblingIndex());
    }
}
