using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;


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

    private void Awake()
    {
        weight = baseWeight;
        accuracy = baseAccuracy;
        size = baseSize;
        bounce = baseBounce;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        SphereCollider col = GetComponent<SphereCollider>();
        material = col.material;
        material.bounciness = Mathf.Clamp(bounce,0,1);
        rb.mass *= weight;
        transform.localScale *= size;
    }

    private void Start()
    {
        
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
                weight = weight + (this.weight * 0.01f * value);
                if (rb==null) rb = GetComponent<Rigidbody>();
                rb.mass = weight;
                break;
            case "accuracy":
                accuracy = accuracy + (accuracy *0.01f* value);
                break;
            case "size":
                size = size + (this.size * 0.01f * value);
                transform.localScale = new Vector3(size, size, size);
                break;
            case "bounce":
                bounce = bounce + (this.bounce * 0.01f * value);
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
        this.weight += (this.weight * 0.01f * weight);
        this.accuracy += (this.accuracy * 0.01f * accuracy);
        this.size += (this.size * 0.01f * size);
        this.bounce += (this.bounce * 0.01f * bounce);
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

    public void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        switch (hit.tag)
        {
            case "Pin":
                Debug.Log("hitPinPush");
                Vector3 forcePosition = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                Vector3 force = (forcePosition - transform.position).normalized * bounce * weight;
                Debug.Log(force);
                hit.GetComponent<Rigidbody>().AddForceAtPosition(force, forcePosition);
                break;
            case "Barrier":
                wallBounce();
                break;
        }
        OnSpecialCollisionEnter(collision);
    }

    private void wallBounce()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x*-1*(bounce/50), rb.linearVelocity.y, rb.linearVelocity.z);
    }


    protected virtual void OnSpecialCollisionEnter(Collision Collision)
    {

    }

    public void setHeld(bool held) 
    { 
        GetComponent<Rigidbody>().isKinematic = held;
    }

    public void throwBall(float power)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        rb.AddForce(transform.forward * 100 * power);
        float variance = Random.Range(-10, 11);
        Debug.Log("variance: " + variance);
        Debug.Log("Variance/accuracy: " + variance/accuracy);
        rb.AddForce(transform.right * (variance/accuracy) * 10);

    }
}
