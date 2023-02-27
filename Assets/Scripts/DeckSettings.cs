using System.Collections.Generic;
using UnityEngine;

public enum DeckType
{
    ForFriends,
    ForCouples
}

[CreateAssetMenu(fileName = "New Deck", menuName = "Decks")]
public class DeckSettings : ScriptableObject
{
    public string deckTitle;
    public DeckType type;
    public bool free;
    public bool adult;
    public List<Question> questions;
    
}

[System.Serializable]
public class Question
{
    public string theme;
    [TextArea(3, 5)]
    public string text;
    public string timer;
}
