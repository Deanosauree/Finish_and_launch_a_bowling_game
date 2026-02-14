using UnityEngine;


[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(SphereCollider))]


public class bowlingBallBase : MonoBehaviour
{
    public float points = 50;
    public float additiveMultiplier = 1.0f;
    public float mulMultiplier = 1.0f;
}
