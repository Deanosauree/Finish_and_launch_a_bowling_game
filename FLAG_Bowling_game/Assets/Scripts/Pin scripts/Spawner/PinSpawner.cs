using Unity.Mathematics;
using UnityEngine;


public class PinSpawner : MonoBehaviour
{
    
    [SerializeField] private Transform PinParent;
    [SerializeField] private Pin[] pins;
    private Transform[] allPinPos;


    private double accumulatedWeights;
    private System.Random rand = new System.Random();



    private void Awake()
    {
        CalculateWeights();
    }

    private void Start()
    {
        SpawnAllPins();
    }

    public void SpawnAllPins()
    {
        allPinPos = new Transform[PinParent.transform.childCount];

        for (int i = 0; i < allPinPos.Length; i++)
        {
            allPinPos[i] = PinParent.GetChild(i);
            SpawnRandomPin(allPinPos[i].transform.position);
        }
    }


    private void SpawnRandomPin(Vector3 position)
    {
        Pin randomPin = pins[GetRandomPinIndex()];

        Instantiate (randomPin.prefab, position, quaternion.identity, transform);
    }

    private int GetRandomPinIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < pins.Length; i++)
        {
            if(pins[i]._weight >= r)
            {
                return i;
            }
        }
        return 0;
    }

    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (var Pin in pins)
        {
            accumulatedWeights += Pin.chance;
            Pin._weight = accumulatedWeights;
        }
    }
}



[System.Serializable]
public class Pin
{
    public GameObject prefab;
    [Range(0f, 100f)] public float chance = 100f;

    [HideInInspector] public double _weight;
}
