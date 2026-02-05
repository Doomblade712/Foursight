using UnityEngine;
using FoursightProductions;
public class Building : MonoBehaviour
{
    public int cost;
    public float health;
    public float damage;
    public DamageType damageType;
    public float attackSpeed;
    public EffectArea damageShape;
    public Sprite buildingSprite;

    public enum DamageType
    {
        Physical
    }

}
