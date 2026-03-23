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
    }

    private void Start()
    {
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

    public void CalculateWeights(float pinChace = 0.0f, float plasticPinChance = 0.0f, float sliverPinChance = 0.0f, 
        float explosivePinChance = 0.0f, float icePinChance = 0.0f, float tungstenPinChance = 0.0f, 
        float tournamentPinChance = 0.0f,float goldenPinChance = 0.0f)
    {
        accumulatedWeights = 0f;
        foreach (var Pin in pins)
        {
            if (Pin.prefab.name == "pin")
            {
                Pin.chance = pinChace;
            }
            else if(Pin.prefab.name == "Plastic Pin")
            {
                Pin.chance = plasticPinChance;
            }
            else if(Pin.prefab.name == "Sliver Pin")
            {
                Pin.chance = sliverPinChance;
            }
            else if(Pin.prefab.name == "Explosive Pin")
            {
                Pin.chance = explosivePinChance;
            }
            else if(Pin.prefab.name == "Ice Pin")
            {
                Pin.chance = icePinChance;
            }
            else if(Pin.prefab.name == "Tungsten Pin")
            {
                Pin.chance = tungstenPinChance;
            }
            else if(Pin.prefab.name == "Tournament Pin")
            {
                Pin.chance = tournamentPinChance;
            }
            else
            {
                Pin.chance = goldenPinChance;
            }
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
