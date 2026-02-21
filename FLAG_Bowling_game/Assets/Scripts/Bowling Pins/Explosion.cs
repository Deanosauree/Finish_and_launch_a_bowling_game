using UnityEngine;

public class Explosion : MonoBehaviour, IisSpecial
{
    public float expForce = 800.0f, radius = 5.0f, upwardsModifier = 2.0f, timeDelay = 1.5f;

    public void DoAbility()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(expForce, explosionPosition, radius, upwardsModifier);
            }
        }
    }
}
