using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int rounds = 10;
    [SerializeField] ballManager ballManager;
    [SerializeField] pinStoreManager pinStoreManager;
    [SerializeField] PinSpawner pinSpawner;

    private int ballsPerRound = 2;
    private int ballsThrown = 0;
    private int round = 0;

    private void Awake()
    {
        ballManager.bowlingStarted.AddListener(bowlingStarted);
    }

    void Start()
    {
        initialise();
    }

    public void initialise()
    {
        ballManager.initialise();
        pinStoreManager.setShopOpen(false);
        Dictionary<string, float> weights = pinStoreManager.getWeights();
        pinSpawner.CalculateWeights(pinChace: 100, plasticPinChance: weights["plasticPin"], explosivePinChance: weights["explosivePin"],
            icePinChance: weights["icePin"], sliverPinChance: weights["silverPin"], tungstenPinChance: weights["tungstenPin"],
            tournamentPinChance: weights["tournamentPin"], goldenPinChance: weights["goldPin"]);
        pinSpawner.SpawnAllPins();
    }

    public void setBallType(GameObject ballPrefab)
    {
        ballManager.setBallType(ballPrefab);
    }

    public void bowlingStarted()
    {
        pinStoreManager.setShopOpen(false);
    }

    public void pinsReset() // CLEAR PINS EITHER BEFORE CALLING AN EVENT THAT CALLS THIS, OR CALL THE CLEAR PIN FUNCTION HERE
    {
        ballManager.destroyBall();
        if (ballsThrown >= ballsPerRound)
        {
            ballsThrown = 0;
            newRound();
            // start up store
        }
        else 
        { 
            ballsThrown++;
            ballManager.respawnBall();
        }
    }

    private void newRound()
    {
        round++;
        pinStoreManager.setShopOpen(true);
        ballManager.spawnBalls();
        Dictionary<string, float> weights = pinStoreManager.getWeights();
        pinSpawner.CalculateWeights(pinChace: 100, plasticPinChance: weights["plasticPin"], explosivePinChance: weights["explosivePin"],
            icePinChance: weights["icePin"], sliverPinChance: weights["silverPin"], tungstenPinChance: weights["tungstenPin"], 
            tournamentPinChance: weights["tournamentPin"], goldenPinChance: weights["goldPin"]);
        pinSpawner.SpawnAllPins();
    }

}
