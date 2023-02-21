using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedDeck : MonoBehaviour, IPointerClickHandler
{
    public static int selectedDeck;

    public GameObject deckContainer;
    public GameObject check;
    public GameObject unCheck;
    public GameObject lockCheck;

    private void OnEnable()
    {
        for (int i = 0; i < deckContainer.transform.childCount; i++)
        {

            if(i == selectedDeck)
            {
                if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck)
                {
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(true);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                }
                
            } else
            {
                if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck)
                {
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(true);
                }
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GetComponent<Button>().interactable) { 
            selectedDeck = transform.parent.GetSiblingIndex();
            Debug.Log($"Deck selected: {selectedDeck}");

            for (int i = 0; i < deckContainer.transform.childCount; i++)
            {
                if (i == selectedDeck)
                {    
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(true);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                }
                else
                {
                    if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck)
                    {
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(true);
                    }
                }

            }
        }
    }
}
