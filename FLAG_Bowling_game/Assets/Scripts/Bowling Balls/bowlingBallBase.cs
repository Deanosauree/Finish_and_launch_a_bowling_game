using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(SphereCollider))]


public abstract class bowlingBallBase : MonoBehaviour
{
    

    public float baseWeight = 1;
    public float baseAccuracy = 1;
    public float baseSize = 1;
    public float baseBounce = 1;

    protected float weight;
    protected float accuracy;
    protected float size;
    protected float bounce;

    protected Rigidbody rb;
    protected PhysicsMaterial material;

    public bowlingBallBase(float weight, float accuracy, float size, float bounce)
    {
    }

    protected abstract void ballInitialise();
    
    private void Start()
    {
        weight = baseWeight;
        accuracy = baseAccuracy; 
        size = baseSize;
        bounce = baseBounce;
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

    public void addStat(string type, float value)
    {
        switch (type)
        {
            case "weight":
                weight = baseWeight + value;
                break;
            case "accuracy":
                accuracy = baseAccuracy + value;
                break;
            case "size":
                size = baseSize + value;
                break;
            case "bounce":
                bounce = baseBounce + value;
                break;
            default:
                Debug.LogError("inproper stat "+ type+". Please use weight, accuracy, size or bounce");
                break;
        }
    }

    public void addStats(float weight, float accuracy, float size, float bounce)
    {
        this.weight += weight;
        this.accuracy += accuracy;
        this.size += size;
        this.bounce += bounce;
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
