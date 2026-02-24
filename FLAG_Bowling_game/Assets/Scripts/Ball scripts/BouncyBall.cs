using UnityEngine;

public class BouncyBall : bowlingBallBase
{
    public BouncyBall(float weight, float accuracy, float size, float bounce) : base(weight, accuracy, size, bounce)
    {
    }

    protected override void ballInitialise()
    {

    }

    protected override void OnSpecialCollisionEnter(Collision Collision)
    {
        GameObject hit = Collision.gameObject;
        if (hit.CompareTag("Pin"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x*-1, rb.linearVelocity.y, rb.linearVelocity.z*2);
        }
    }
}
