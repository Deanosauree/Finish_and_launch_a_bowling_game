using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Localization.Plugins.XLIFF.V12;

public class BowlingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int rounds = 10;
    [SerializeField] ballManager ballManager;
    [SerializeField] pinStoreManager pinStoreManager;
    [SerializeField] PinSpawner pinSpawner;
    [SerializeField] float pinResetTime;
    [SerializeField] GameObject controlsUI;

    private int ballsPerRound = 2;
    private int ballsThrown = 0;
    private int round = 0;
    private float totalPoints;
    private bool controlsEnabled = true;

    private bool roundReady = false;

    private void Awake()
    {
        ballManager.bowlingStarted.AddListener(bowlingStarted);
        PointCalc.PinDroppedEvent += pinDropped;
        ballManager.ballSelected.AddListener(startRound);
        if (PlayerInfo.bowlingBall != null)
        {
            ballManager.setBallType(PlayerInfo.bowlingBall);
        }
        else 
        {
            Debug.Log("No Ball Apparent");
        }
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
        showControls(true);
    }

    public void setBallType(GameObject ballPrefab)
    {
        ballManager.setBallType(ballPrefab);
    }

    public void bowlingStarted()
    {
        pinStoreManager.setShopOpen(false);
    }

    public void pinDropped()
    {
        CancelInvoke("pinsReset");
        Invoke("pinsReset", pinResetTime);
        Debug.Log("Pin Dropped");
    }

    public void showControls(bool arg)
    {
        if (controlsEnabled)
        {
            controlsUI.SetActive(arg);
        }
        else
        {
            controlsUI.SetActive(false);
        }
        
    }

    public void pinsReset() // CLEAR PINS EITHER BEFORE CALLING AN EVENT THAT CALLS THIS, OR CALL THE CLEAR PIN FUNCTION HERE
    {
        float points;
        float multi;
        int dropped;
        float adPoints;
        float adMulti;

        (points, multi, dropped) = PointCalc.getPointsMultiPins();
        (adPoints, adMulti) = ballManager.getAdditionalScore();

        points += adPoints * dropped;
        multi += adMulti * dropped;
        
        double finalScore = points * multi;
        totalPoints += points;
        pinStoreManager.addPoints(points);
        Debug.Log($"The score is: {finalScore}, Balls thrown in {ballsThrown}");

        ballManager.destroyBall();
        ballManager.resetShuffle();
        if (dropped == 100)
        {
            ballsThrown = 0;
            newRound();
        }
        else
        {
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
        
    }

    private void newRound()
    {
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            Destroy(pin.gameObject);
        }
        round++;
        pinStoreManager.setShopOpen(true);
        ballManager.spawnBalls();
        roundReady = true;
        
    }

    public void startRound()
    {
        if (roundReady)
        {
            Dictionary<string, float> weights = pinStoreManager.getWeights();
            pinSpawner.CalculateWeights(pinChace: 100, plasticPinChance: weights["plasticPin"], explosivePinChance: weights["explosivePin"],
                icePinChance: weights["icePin"], sliverPinChance: weights["silverPin"], tungstenPinChance: weights["tungstenPin"],
                tournamentPinChance: weights["tournamentPin"], goldenPinChance: weights["goldPin"]);
            pinSpawner.SpawnAllPins();
        }
    }
        
}
