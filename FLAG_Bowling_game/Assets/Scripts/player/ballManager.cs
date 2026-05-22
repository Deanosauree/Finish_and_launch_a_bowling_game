using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class ballManager: MonoBehaviour
{
    [SerializeField] Transform ballHoldLocation;
    [SerializeField] upgradeUIController upgradeUI;
    [SerializeField] Transform[] ballDisplayPositions;
    [SerializeField] GameObject ballDisplay;

    [SerializeField] GameObject BowlingBallPrefab;
    [SerializeField] float throwPower = 1;
    [SerializeField] int upgradePercent = 50;
    [SerializeField] float percentOfWeightUpgToSpeed = 50;
    private bowlingBallBase bowlingBall;
    private bool ballHeld = false;

    private bowlingBallBase[] ballChoices;
    private string[] chosenUpgrades = new string[3];
    private LocalizedString[] chosenUpgradesLocale = new LocalizedString[3];
    
    private cameraController playerCam;

    private (float, float) pointsAndMulti;
    public bool readyToBowl = true;
    public UnityEvent bowlingStarted;
    public UnityEvent ballSelected;
    public bool roundRunning = false;
    private Dictionary<string, float> bowlingBallData = new Dictionary<string, float> { { "weight", 0 }, { "accuracy", 0 }, { "size", 0 }, { "bounce", 0 }, { "speed", 0 } };

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void initialise()
    {
        spawnBalls();
    }

    public (float, float) getAdditionalScore()
    {
        return pointsAndMulti;
    }

    void Awake()
    {
        upgradeUI.upgradePressed.AddListener(upgradeSelected);
        playerCam = GetComponent<cameraController>();
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }
    void OnLocaleChanged(Locale newLocale)
    {
        upgradeUI.SetUpgradeNames($"{chosenUpgradesLocale[0].GetLocalizedString()} +{upgradePercent}%", $"{chosenUpgradesLocale[1].GetLocalizedString()} +{upgradePercent}%", $"{chosenUpgradesLocale[2].GetLocalizedString()} +{upgradePercent}%");
    }


    // Update is called once per frame
    void Update()
    {
        if (ballHeld)
        {
            bowlingBall.setLocation(ballHoldLocation.position, ballHoldLocation.rotation);
        }
    }

    public void spawnBalls()
    {
        string[] upgrades = getRandomUpgrades();
        spawnUpgradedBalls(upgrades[0], upgrades[1], upgrades[2], new float[] { upgradePercent, upgradePercent, upgradePercent });
    }

    public void setBowlReady()
    {
        readyToBowl = true;
    }

    public void setBallType(GameObject prefab)
    {
        BowlingBallPrefab = prefab;
    }
    public void throwBall()
    {
        if (ballHeld && readyToBowl)
        {
            if (PlayerInfo.bowling)
            {
                bowlingBall.setLocation(new Vector3(ballHoldLocation.position.x, ballHoldLocation.position.y - 0.6f, ballHoldLocation.position.z), ballHoldLocation.rotation);
                bowlingBall.throwBall(throwPower);
                ballHeld = false;
                roundRunning = true;
                PlayerInfo.bowling = false;
                PlayerInfo.bowling = false;
                playerCam.showLine(false);
            }
            else 
            {
                PlayerInfo.bowling = true;
                bowlingStarted.Invoke();
                PlayerInfo.bowling = true;
                
                playerCam.showLine(true);
            }
            
        }
    }

    public void resetShuffle()
    {
        playerCam.resetShufflePan();
    }
    public void destroyBall()
    {
        if ( bowlingBall != null)
        {
            if (roundRunning)
            {
                PointCalc.PinDropped(0, 1);
            }
            bowlingBall.destroyBall.RemoveAllListeners();
            Destroy(bowlingBall.gameObject);
            bowlingBall = null;
            ballHeld = false;
        }
        
    }
    private void setPointsAndMulti()
    {
        if (pointsAndMulti != (null, null))
        {
            pointsAndMulti = (bowlingBall.getAddPoints(), bowlingBall.getAddMulti());
        }
    }

    public void respawnBall()
    {
        if (bowlingBall != null)
        {
            destroyBall();
        }
        
        bowlingBallBase ball = Instantiate(BowlingBallPrefab, this.transform, true).GetComponent<bowlingBallBase>();
        bowlingBall = ball;
        ball.setLocation(ballHoldLocation.position, ballHoldLocation.rotation);
        ball.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"], bowlingBallData["speed"]);
        ballHeld = true;
        ball.setHeld(ballHeld);
        setPointsAndMulti();
        Invoke("setBowlReady", 5);

    }

    public void upgradeSelected(int index)
    {
        if (!ballHeld)
        {
            if (bowlingBall != null)
            {
                destroyBall();
            }
            bowlingBall = ballChoices[index];
            bowlingBallData[chosenUpgrades[index]] += upgradePercent;
            if (chosenUpgrades[index] == "weight")
            {
                bowlingBallData["speed"] += upgradePercent * (percentOfWeightUpgToSpeed/100);
            }
            bowlingBall.destroyBall.AddListener(destroyBall);
            startHoldingBall();
            for (int i = 0; i < ballChoices.Length; i++)
            {
                if (i != index)
                {
                    Destroy(ballChoices[i].gameObject);
                }
            }
            upgradeUI.SetUpgradesVisible(false);
            ballChoices = null;
            setPointsAndMulti();
            ballSelected.Invoke();
        }
    }

    private void spawnUpgradedBalls(string firstUpgrade, string secondUpgrade, string thirdUpgrade, float[] values)
    {
        bowlingBallBase firstBall = Instantiate(BowlingBallPrefab, ballDisplayPositions[0]).GetComponent<bowlingBallBase>();
        bowlingBallBase secondBall = Instantiate(BowlingBallPrefab, ballDisplayPositions[1]).GetComponent<bowlingBallBase>();
        bowlingBallBase thirdball = Instantiate(BowlingBallPrefab, ballDisplayPositions[2]).GetComponent<bowlingBallBase>();

        firstBall.setLocation(ballDisplayPositions[0].position, ballDisplayPositions[0].rotation);
        secondBall.setLocation(ballDisplayPositions[1].position, ballDisplayPositions[1].rotation);
        thirdball.setLocation(ballDisplayPositions[2].position, ballDisplayPositions[2].rotation);

        firstBall.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"], bowlingBallData["speed"]);
        secondBall.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"], bowlingBallData["speed"]);
        thirdball.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"], bowlingBallData["speed"]);

        firstBall.addStat(firstUpgrade, values[0]);
        secondBall.addStat(secondUpgrade, values[1]);
        thirdball.addStat(thirdUpgrade, values[2]);

        chosenUpgrades[0] = firstUpgrade;
        chosenUpgrades[1] = secondUpgrade;
        chosenUpgrades[2] = thirdUpgrade;

        chosenUpgradesLocale[0] = new LocalizedString("string table", firstUpgrade);
        chosenUpgradesLocale[1] = new LocalizedString("string table", secondUpgrade);
        chosenUpgradesLocale[2] = new LocalizedString("string table", thirdUpgrade);

        upgradeUI.SetUpgradeNames($"{chosenUpgradesLocale[0].GetLocalizedString()} +{values[0]}%", $"{chosenUpgradesLocale[1].GetLocalizedString()} +{values[1]}%", $"{chosenUpgradesLocale[2].GetLocalizedString()} +{values[2]}%");
        upgradeUI.SetUpgradesVisible(true);
        ballChoices = new bowlingBallBase[] { firstBall, secondBall, thirdball };
    }

    private string[] getRandomUpgrades()
    {
        string[] choices = new string[] { "weight", "accuracy", "size", "bounce", "speed"};
        string[] chosenBalls = new string[3];
        
        for (int i = 0; i < chosenBalls.Length; i++) 
        {
            bool picking = true;
            while (picking)
            {
                int upgradeChoice = Random.Range(0, 5);
                if (!(chosenBalls.Contains(choices[upgradeChoice])))
                {
                    picking = false;
                    chosenBalls[i] = choices[upgradeChoice];
                }
            }
        }
        return chosenBalls;

    }

    void startHoldingBall()
    {
        bowlingBall.setHeld(true);
        ballHeld = true;
    }

    
}
