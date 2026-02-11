using NUnit.Framework;
using UnityEngine;
using FoursightProductions;
using System.Collections.Generic;
using TMPro;

public class DrawPileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    private int startingHandSize;
    private int maxHandSize;
    private int currentHandSize;


    private int currentIndex = 0;
    private HandManager handManager;
    private DiscardManager discardManager;
    public TextMeshProUGUI drawPileCounter;
    private GameManager gameManager;

    void Start()
    {
        handManager = FindFirstObjectByType<HandManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    public void roundSetup(int numberOfCardsToDraw,int setMaxHandsize)
    {
        maxHandSize = setMaxHandsize;
        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            DrawCard(handManager);
        }
    }

    public void DrawCard(HandManager handManager)
    {

        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
        }
        if (drawPile.Count > 0)
        {
            if (currentHandSize < maxHandSize)
            {
                Card nextCard = drawPile[currentIndex];
                handManager.AddCardToHand(nextCard);
                drawPile.RemoveAt(currentIndex);
                if (drawPile.Count > 0) currentIndex %= drawPile.Count;
            }
        }
    }

    private void RefillDeckFromDiscard()
    {
        if(discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }

        if(discardManager != null && discardManager.discardCardsCount > 0)
        {
            drawPile = discardManager.AllCardsInDiscard(true);
            Utility.Shuffle(drawPile);
            currentIndex = 0;
        }
    }

    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();
    }
}
