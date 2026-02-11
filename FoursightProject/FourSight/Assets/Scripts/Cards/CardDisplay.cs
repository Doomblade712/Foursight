using UnityEngine;
using FoursightProductions;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Sprite cardImage;
    public Sprite backgroundImage;
    public Sprite cardTextBackgroundImage;
    public TMP_Text nameText;
    public TMP_Text cardText;

    public void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        nameText.text = cardData.cardName;
        cardText.text = cardData.cardText;
       
    }


}
