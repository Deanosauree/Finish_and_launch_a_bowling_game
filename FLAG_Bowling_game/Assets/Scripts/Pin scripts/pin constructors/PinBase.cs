using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PinBase : MonoBehaviour
{
    //base class shouldn't need any changes going forward except base stats (follows strategy patern)
    public float points =  10;
    public float GMultiplier = 1;
    public float weight = 5;
    public bool counted = false;
    public IisSpecial abilityType;
    public bool isMultAdded = false;

    private bool played = false;
    private bool evenRound = false;
    private Rigidbody rb;
    private Vector3 startPos;
    protected float checkOffset;
    private AudioSource auSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = weight;
        startPos = transform.position;
        evenRound = PointCalc.evenRound;
        checkOffset = ((float)Random.Range(100, 300)) / 100;
        auSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        InvokeRepeating("checkTilted", checkOffset, 3);
    }

    public void TryDoAbility()
    {
        abilityType?.DoAbility();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!played)
        {
            if (other.CompareTag("Pin"))
            {
                auSource.Play();
                played = true;
            }

        }
    }

    private void checkTilted()
    {
        float difference = Quaternion.Angle(Quaternion.identity, transform.rotation);
        bool round = PointCalc.evenRound;
        if ( difference > 45 && !counted && round == evenRound)
        {
            PointCalc.PinDropped(points, GMultiplier);
            counted = true;
            auSource.Play();
            played = true;
        }
        if (evenRound != round)
        {
            if (counted)
            {
                Destroy(gameObject);
            }
            else
            {
                rb.isKinematic = true;
                transform.position = startPos;
                transform.rotation = Quaternion.identity;
                Invoke("setMoving", 5-checkOffset);
            }
            evenRound = PointCalc.evenRound;
        }
    }

    private void setMoving()
    {
        rb.isKinematic = false;
        played = false;
    }
}

public interface IisSpecial
{
    void DoAbility();
}