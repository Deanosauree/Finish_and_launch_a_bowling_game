using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "PinCostAttributes", menuName = "Scriptable Objects/PinCostAttributes")]
public class PinCostAttributes : ScriptableObject
{
    public LocalizedString pinName;
    public LocalizedString description;
    public Sprite card;
    public float pinPrice = 1;
    public float pinChance;
    public int amountPurchasable =  1;
    public float pinProbabilityIncrease = 1;
    public float pinPriceMultiplier = 1;
}
