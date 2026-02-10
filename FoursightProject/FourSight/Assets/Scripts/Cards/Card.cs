using UnityEngine;
using UnityEditor;

namespace FoursightProductions
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public Sprite cardbackground;
        public Sprite cardArt;
        public string cardName;
        public CardType cardType;
        public int cardCost;
        public GameObject towerPrefab;
        public string cardText;
        
        public enum CardType
        {
            Building,
            Utility
        }
    }
}
