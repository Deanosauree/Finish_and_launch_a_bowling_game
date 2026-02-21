using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(SphereCollider))]


public abstract class bowlingBallBase : MonoBehaviour
{
    public float weight = 1;
    public float accuracy = 1;
    public float size = 1;
    public float bounce = 1;

    protected float baseWeight;
    protected float baseAccuracy;
    protected float baseSize;
    protected float baseBounce;

    protected Rigidbody rb;
    protected PhysicsMaterial material;

    public bowlingBallBase(float weight, float accuracy, float size, float bounce)
    {
    }

    protected abstract void ballInitialise();
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        SphereCollider col = GetComponent<SphereCollider>();
        material = col.material;
        transform.localScale = transform.localScale * size;
        material.bounciness = bounce;
        rb.mass *= weight;
        transform.localScale *= size;
        ballInitialise();
    }
    public void setLocation(Vector3 location, Quaternion rotation)
    {
        
        transform.position = location;
        transform.rotation = rotation;
    }

    public void addStats(float weight, float accuracy, float size, float bounce)
    {

        weight = baseWeight + weight;
        accuracy = baseAccuracy + accuracy;
        size = baseSize + size;
        bounce = baseBounce + bounce;
    }

    public void setHeld(bool held) 
    { 
        GetComponent<Rigidbody>().isKinematic = held;
    }

    public void throwBall(float power)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        rb.AddForce(transform.forward * 100 * power);

    }
}
