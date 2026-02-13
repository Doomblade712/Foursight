using FoursightProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize = 6;
    public int maxHandSize = 9;
    public int currentHandSize = 0;
    private HandManager handManager;
    private DrawPileManager drawPileManager;
    private bool startMap = true;

    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);
    }

    void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindFirstObjectByType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }
    }

    void Update()
    {
        if (startMap)
        {
            BattleSetup();
        }
    }

    public void BattleSetup()
    {
        handManager.roundSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allCards);
        drawPileManager.roundSetup(startingHandSize, maxHandSize);
        startMap = false;
    }



}
