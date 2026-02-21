using Unity.Mathematics;
using UnityEngine;

public class PinExplosivePin : BowlingPinBase
{

    public GameObject exp;


    public PinExplosivePin()//constractor
    {
        //explosion on contact with other pins or with the ball
        //both can happen if we want more pin variations
        weight = 30;
    }

    private void Start()
    {
        abilityType = gameObject.AddComponent<Explosion>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pin") || other.CompareTag("Ground"))
        {
            TryDoAbility();
            GameObject explosion = Instantiate(exp, transform.position, quaternion.identity);
            Destroy(explosion, 2);
        }
    }
}
