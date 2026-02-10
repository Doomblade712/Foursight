using UnityEngine;
using System.Collections.Generic;
using FoursightProductions;
using NUnit.Framework;
using System;
public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handTransform;
    public float fanSpread = 4.27f;
    public float cardSpacing = -184.1f;
    public float verticalSpacing = 61.3f;
    public int maxHandSize;
    public List<GameObject> cardsInHand = new List<GameObject>();
    void Start()
    {

    }

    void Update()
    {
        //UpdateHandVisuals();

    }

    public void roundSetup(int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
    }

    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        newCard.GetComponent<CardDisplay>().cardData = cardData;

        UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f,0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f,0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = (verticalSpacing * (1 - normalizedPosition * normalizedPosition));
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
