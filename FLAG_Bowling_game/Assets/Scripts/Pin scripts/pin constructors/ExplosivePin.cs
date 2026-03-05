using Unity.Mathematics;
using UnityEngine;

public class ExplosivePin : PinBase
{
    public GameObject exp;
    private bool canExplode = true;

    public ExplosivePin()//constractor
    {
        weight = 30;
    }

    private void Start()
    {
        abilityType = gameObject.AddComponent<Explosion>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Pin") || other.CompareTag("Ground")) && canExplode == true)
        {
            TryDoAbility();
            GameObject explosion = Instantiate(exp, transform.position, quaternion.identity);
            PointCalc.PinDropped(points, GMultiplier);
            canExplode = false;
            Destroy(explosion, 2);
        }
    }
}
