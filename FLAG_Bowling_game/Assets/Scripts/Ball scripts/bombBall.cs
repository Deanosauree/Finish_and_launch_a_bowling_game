using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class bombBall : bowlingBallBase
{
    public bombBall(float weight, float accuracy, float size, float bounce, float speed) : base(weight, accuracy, size, bounce, speed)
    {
    }
    public GameObject exp;
    public float expForce = 800.0f, radius = 5.0f, upwardsModifier = 2.0f;
    public int hitsToKaboom = 10;
    private int collidersHit = 0;

    protected override void ballInitialise()
    {
    }

    protected override void OnSpecialCollisionEnter(Collision Collision)
    {
        GameObject collided = Collision.gameObject;
        if (collided.name == "Explosive Pin")
        {
            DoAbility();
        }
        else if (collided.CompareTag("Pin"))
        {
            collidersHit++;
            Debug.Log(collidersHit.ToString());
            if (collidersHit == hitsToKaboom)
            {
                DoAbility();
            }
        }
    }

    

    private void DoAbility()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        GameObject explosion = Instantiate(exp, transform.position, Quaternion.identity);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(expForce, explosionPosition, radius, upwardsModifier);
            }
        }
        Destroy(explosion, 2);
        destroyBall.Invoke();
    }
}
