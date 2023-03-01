using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeEffect : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Vector3 _initialPosition;
    private float _distanceMoved;
    private bool _swipeLeft;
    private float eventDelta;
    private static bool directionRight;
    private static bool directionLeft;

    private float speed;

    public UnityEvent swipeRight;
    public UnityEvent swipeLeft;

    public UnityEvent swipingRight;
    public UnityEvent swipingLeft;

    public UnityEvent cancelSwipe;

    public void OnDrag(PointerEventData eventData)
    {
        //speed = Mathf.Clamp(eventData.delta.x * Time.deltaTime, -1f, 1f);
        speed = eventData.delta.x * Time.deltaTime;

        if (eventData.delta.x > 0 && ((transform.localEulerAngles.z >= 0 && transform.localEulerAngles.z <= 5) || (transform.localEulerAngles.z <= 365 && transform.localEulerAngles.z >= 360)))
        {
            if (!directionRight)
            {
                //if (GetComponent<RectTransform>().pivot.x == 0.5)
                //{
                //    GetComponent<RectTransform>().pivot = new Vector2(1, 0);
                //    transform.localPosition = new Vector2(transform.localPosition.x + GetComponent<RectTransform>().rect.width / 2, transform.localPosition.y);
                //}
                //else
                //{
                //    GetComponent<RectTransform>().pivot = new Vector2(1, 0);
                //    transform.localPosition = new Vector2(transform.localPosition.x + GetComponent<RectTransform>().rect.width, transform.localPosition.y);
                //}
                directionRight = true;
                directionLeft = false;
            }
        }
        if (eventData.delta.x < 0 && ((transform.localEulerAngles.z >= 0 && transform.localEulerAngles.z <= 5) || (transform.localEulerAngles.z <= 365 && transform.localEulerAngles.z >= 360)))
        {
            if (!directionLeft)
            {
                //if (GetComponent<RectTransform>().pivot.x == 0.5)
                //{
                //    GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                //    transform.localPosition = new Vector2(transform.localPosition.x - GetComponent<RectTransform>().rect.width / 2, transform.localPosition.y);
                //}
                //else
                //{
                //    GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                //    transform.localPosition = new Vector2(transform.localPosition.x - GetComponent<RectTransform>().rect.width, transform.localPosition.y);
                //}
                directionRight = false;
                directionLeft = true;
            }
        }

        if(directionLeft)
        {
            swipingLeft?.Invoke();
        }

        if(directionRight)
        {
            swipingRight?.Invoke();
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y, transform.localEulerAngles.z - speed);
        if(transform.localEulerAngles.z > 90 && 360 - transform.localEulerAngles.z > 45)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 315);
        }

        if(transform.localEulerAngles.z > 45 && transform.localEulerAngles.z < 90)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 45);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localEulerAngles; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.delta.x == 0 && (transform.localEulerAngles.z > 5 && transform.localEulerAngles.z < 90))
        {
            swipeLeft?.Invoke();
            Debug.Log("Left");
        }


        if (eventData.delta.x == 0 && (transform.localEulerAngles.z > 90 && 360 - transform.localEulerAngles.z > 5))
        { 
            swipeRight?.Invoke();
            Debug.Log("Right");
        }

        if ((eventData.delta.x == 0 && (transform.localEulerAngles.z > 90 && (360 - transform.localEulerAngles.z >= 0 && 360 - transform.localEulerAngles.z <= 5))) || (eventData.delta.x == 0 && ((transform.localEulerAngles.z >= 0 && transform.localEulerAngles.z <= 5) && transform.localEulerAngles.z < 90)))
        {
            cancelSwipe?.Invoke();
            Debug.Log("Cancel");
        }

        transform.localEulerAngles = _initialPosition;
        cancelSwipe?.Invoke();
    }
 
}
