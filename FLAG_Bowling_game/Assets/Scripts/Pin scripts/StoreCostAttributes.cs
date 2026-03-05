using UnityEngine;

[CreateAssetMenu(fileName = "PinCostAttributes", menuName = "Scriptable Objects/PinCostAttributes")]
public class StoreCostAttributes : ScriptableObject
{
    public float pinName;
    public float basePrice;
    public float costMultiplier;
    public int purchaseCount;
    public float appearanceChance;
}
