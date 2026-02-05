using UnityEngine;
using FoursightProductions;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardImage;
    public Image backgroundImage;
    public Image cardTextBackgroundImage;
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
