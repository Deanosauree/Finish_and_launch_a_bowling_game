using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
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

    private bowlingBallBase bowlingBall;
    private bool ballHeld = false;
    private bowlingBallBase[] ballChoices;

    private Dictionary<string, float> bowlingBallData = new Dictionary<string, float> { { "weight", 0 }, { "accuracy", 0 }, { "size", 0 }, { "bounce", 0 }, { "speed", 0 } };
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void initialise()
    {
        spawnBalls();
    }

    void Awake()
    {
        upgradeUI.upgradePressed.AddListener(upgradeSelected);
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
        spawnUpgradedBalls(upgrades[0], upgrades[1], upgrades[2], new float[] { 50, 50, 50 });
    }

    public void setBallType(GameObject prefab)
    {
        BowlingBallPrefab = prefab;
    }
    public void throwBall()
    {
        if (ballHeld)
        {
            Debug.Log("throwing");
            bowlingBall.throwBall(throwPower);
            ballHeld = false;
        }
    }

    public void destroyBall()
    {
        bowlingBall.destroyBall.RemoveAllListeners();
        Destroy(bowlingBall.gameObject);
        bowlingBall = null;
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
            bowlingBall.destroyBall.AddListener(destroyBall);
            startHoldingBall();
            for (int i = 0; i < ballChoices.Length; i++)
            {
                if (i != index)
                {
                    Destroy(ballChoices[i].gameObject);
                    Debug.Log("Destroying Ball " + index);
                }
            }
            upgradeUI.SetUpgradesVisible(false);
            ballChoices = null;
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

         LocalizedString firstLocalised = new LocalizedString("string table", firstUpgrade);
         LocalizedString secondLocalised = new LocalizedString("string table", secondUpgrade);
         LocalizedString thirdLocalised = new LocalizedString("string table", thirdUpgrade);

        upgradeUI.SetUpgradeNames($"{firstLocalised.GetLocalizedString()} +{values[0]}%", $"{secondLocalised.GetLocalizedString()} +{values[1]}%", $"{thirdLocalised.GetLocalizedString()} +{values[2]}%");
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
