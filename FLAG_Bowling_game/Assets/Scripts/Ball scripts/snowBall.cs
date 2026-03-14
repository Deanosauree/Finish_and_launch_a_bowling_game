using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class snowBall : bowlingBallBase
{
    private float goalSize;
    [SerializeField] float growSpeed;
    [SerializeField] float growPerPin;
    public snowBall(float weight, float accuracy, float size, float bounce, float speed) : base(weight, accuracy, size, bounce, speed)
    {
    }

    protected override void ballInitialise()
    {
        enabled = false;
        goalSize = size;
    }

    protected override void OnSpecialCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.CompareTag("Pin"))
        {
            PinBase pin = collided.GetComponent<PinBase>();
            if (collided.name == "Snow Pin")
            {
                Debug.Log($"Growing due to {collided.name}");
                Destroy(collided);
                goalSize += growPerPin;
                enabled = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (size < goalSize)
        {
            size += growSpeed;
            transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
        }
        else 
        {
            enabled = false;
        }
    }

}
