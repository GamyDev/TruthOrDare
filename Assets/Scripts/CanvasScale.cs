using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScale : MonoBehaviour
{
    [SerializeField] private float aspectRatio;
    [SerializeField] private RectTransform panelAddPlayers;
    [SerializeField] private RectTransform panelSpeakerphone;
    [SerializeField] private RectTransform panelSubscription1;
    [SerializeField] private RectTransform panelSubscription2;



    [SerializeField] private float addPlayerPanelSize;
    [SerializeField] private float addSpeakerphonePanelSize;
    [SerializeField] private float addSubscriptionPanelSize;

    void Start()
    {

        aspectRatio = (float)Screen.height / Screen.width;

        if (aspectRatio >= 2.22222)
        {
            addPlayerPanelSize = 1600;
            addSpeakerphonePanelSize = 1700;

        }

        else if (aspectRatio >= 2.16355)
        {
            addPlayerPanelSize = 1550;
            addSpeakerphonePanelSize = 1650;
        }

        else if (aspectRatio >= 2.11111)
        {
            addPlayerPanelSize = 1500;
            addSpeakerphonePanelSize = 1600;
        }

        else if (aspectRatio >= 2.05555)
        {
            addPlayerPanelSize = 1450;
            addSpeakerphonePanelSize = 1550;
        }

        else if (aspectRatio >= 2)
        {
            addPlayerPanelSize = 1400;
            addSpeakerphonePanelSize = 1500;
            addSubscriptionPanelSize = 20;
        }

        else if (aspectRatio >= 1.77777)
        {
            addPlayerPanelSize = 1300;
            addSpeakerphonePanelSize = 1400;
            addSubscriptionPanelSize = 50;
        }

        else if (aspectRatio >= 1.6)
        {
            addPlayerPanelSize = 1250;
            addSpeakerphonePanelSize = 1350;
            addSubscriptionPanelSize = 70;
        }

        else if (aspectRatio >= 1.522849)
        {
            addPlayerPanelSize = 1200;
            addSpeakerphonePanelSize = 1300;
            addSubscriptionPanelSize = 100;
        }

        else if (aspectRatio >= 1.43165)
        {
            addPlayerPanelSize = 1150;
            addSpeakerphonePanelSize = 1250;
            addSubscriptionPanelSize = 110;
        }

        else if (aspectRatio >= 1.33333)
        {
            addPlayerPanelSize = 1100;
            addSpeakerphonePanelSize = 1200;
            addSubscriptionPanelSize = 120;
        }


        panelAddPlayers.sizeDelta = new Vector2(panelAddPlayers.sizeDelta.x, addPlayerPanelSize);
        panelSpeakerphone.sizeDelta = new Vector2(panelSpeakerphone.sizeDelta.x, addSpeakerphonePanelSize);
        panelSubscription1.anchoredPosition = new Vector2(transform.position.x, panelSubscription1.anchoredPosition.y + addSubscriptionPanelSize);
        panelSubscription2.anchoredPosition = new Vector2(transform.position.x, panelSubscription2.anchoredPosition.y - addSubscriptionPanelSize);

    }



}
