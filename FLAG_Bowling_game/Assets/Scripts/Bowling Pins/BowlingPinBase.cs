using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BowlingPinBase : MonoBehaviour
{
    //base class shouldn't need any changes going forward except base stats (follows strategy patern)
    public float points = 50;
    public float additiveMultiplier = 1.0f; //additive multiplier (1+1+1)
    public float mulMultiplier = 1.0f;  //multiplicative multiplier (1*1*1)
    public IisSpecial abilityType;

    public void TryDoAbility()
    {
        abilityType?.DoAbility(points, additiveMultiplier, mulMultiplier);
    }
}

public interface IisSpecial
{
    void DoAbility(float points, float additiveMultiplier, float mulMultiplier);
}



