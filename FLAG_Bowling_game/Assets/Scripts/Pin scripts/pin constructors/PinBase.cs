using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PinBase : MonoBehaviour
{
    //base class shouldn't need any changes going forward except base stats (follows strategy patern)
    public float points =  10;
    public float GMultiplier = 1;
    public float weight = 5;
    private bool counted = false;
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


    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Pin")  || other.CompareTag("Ground")) && counted == false)
        {
            PointCalc.PinDropped(points, GMultiplier);
            counted = true;
            Destroy(this, 1);
        }
    }
}

public interface IisSpecial
{
    void DoAbility();
}