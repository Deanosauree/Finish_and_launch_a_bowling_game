using UnityEngine;

[CreateAssetMenu(fileName = "PinCostAttributes", menuName = "Scriptable Objects/PinCostAttributes")]
public class PinCostAttributes : ScriptableObject
{
    public string pinName;
    public float pinPrice;
    public float pinChance;
    public int amountPurchasable;
    public float pinProbabilityIncrease;
    public float pinPriceMultiplier;
}
