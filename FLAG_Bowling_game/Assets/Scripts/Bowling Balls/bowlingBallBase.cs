using UnityEngine;


[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(SphereCollider))]


public abstract class bowlingBallBase : MonoBehaviour
{
    public float points = 0;
    public float additiveMultiplier = 0;
    public float mulMultiplier = 0;

    public bowlingBallBase(float points, float additiveMultiplier, float mulMultiplier)
    {
    }

    public void setLocation(Vector3 location, Quaternion rotation)
    {
        transform.position = location;
        transform.rotation = rotation;
    }

    public void setHeld(bool held) 
    { 
        GetComponent<Rigidbody>().isKinematic = held;
    }
}
