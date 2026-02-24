using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BowlingPinBase : MonoBehaviour
{
    //base class shouldn't need any changes going forward except base stats (follows strategy patern)
    public float points =  10;
    public float GMultiplier = 1;
    public float weight = 5;
    public IisSpecial abilityType;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = weight;
    }

    public void TryDoAbility()
    {
        abilityType?.DoAbility();
    }
}

public interface IisSpecial
{
    void DoAbility();
}