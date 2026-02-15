using UnityEngine;


[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(SphereCollider))]


public abstract class bowlingBallBase : MonoBehaviour
{
    public float points = 0;
    public float additiveMultiplier = 0;
    public float mulMultiplier = 0;

    public bowlingBallBase()
    {
    }
}
