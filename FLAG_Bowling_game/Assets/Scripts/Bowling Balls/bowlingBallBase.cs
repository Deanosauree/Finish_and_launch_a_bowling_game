using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(SphereCollider))]


public abstract class bowlingBallBase : MonoBehaviour
{
    public UnityEvent destroyBall;

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
                if (rb==null) rb = GetComponent<Rigidbody>();
                rb.mass += value;
                break;
            case "accuracy":
                accuracy = baseAccuracy + value;
                break;
            case "size":
                size = baseSize + value;
                transform.localScale += new Vector3(value, value, value);
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
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        if (material==null) { material = GetComponent<SphereCollider>().material; }
        this.weight += weight;
        this.accuracy += accuracy;
        this.size += size;
        this.bounce += bounce;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other != null) 
        { 
            if (other.CompareTag("killBox"))
            {
                destroyBall.Invoke();
            }
        
        }
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
