
using UnityEngine;
using FoursightProductions;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int currentIndex = 0;

    void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards");
        allCards.AddRange(cards);
    }
    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            return;
        }
        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex + 1) % allCards.Count;
    }
}
