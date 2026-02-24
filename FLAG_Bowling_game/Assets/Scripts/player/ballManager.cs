using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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

    private Dictionary<string, float> bowlingBallData = new Dictionary<string, float> { { "weight", 0 }, { "accuracy", 0 }, { "size", 0 },  { "bounce", 0 } };
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeUI.upgradePressed.AddListener(upgradeSelected);
        spawnUpgradedBalls("weight", "accuracy", "size", new float[] { 50, 50, 50 });

    }

    // Update is called once per frame
    void Update()
    {
        if (ballHeld)
        {
            bowlingBall.setLocation(ballHoldLocation.position, ballHoldLocation.rotation);
        }
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

    public void spawnUpgradedBalls(string firstUpgrade, string secondUpgrade, string thirdUpgrade, float[] values)
    {
        bowlingBallBase firstBall = Instantiate(BowlingBallPrefab, ballDisplayPositions[0]).GetComponent<bowlingBallBase>();
        bowlingBallBase secondBall = Instantiate(BowlingBallPrefab, ballDisplayPositions[1]).GetComponent<bowlingBallBase>();
        bowlingBallBase thirdball = Instantiate(BowlingBallPrefab, ballDisplayPositions[2]).GetComponent<bowlingBallBase>();

        firstBall.setLocation(ballDisplayPositions[0].position, ballDisplayPositions[0].rotation);
        secondBall.setLocation(ballDisplayPositions[1].position, ballDisplayPositions[1].rotation);
        thirdball.setLocation(ballDisplayPositions[2].position, ballDisplayPositions[2].rotation);

        firstBall.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"]);
        secondBall.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"]);
        thirdball.addStats(bowlingBallData["weight"], bowlingBallData["accuracy"], bowlingBallData["size"], bowlingBallData["bounce"]);

        firstBall.addStat(firstUpgrade, values[0]);
        secondBall.addStat(secondUpgrade, values[1]);
        thirdball.addStat(thirdUpgrade, values[2]);

        upgradeUI.SetUpgradeNames($"{firstUpgrade} +{values[0]}%", $"{secondUpgrade} +{values[1]}%", $"{thirdUpgrade} +{values[2]}%");
        upgradeUI.SetUpgradesVisible(true);
        ballChoices = new bowlingBallBase[] { firstBall, secondBall, thirdball };
    }

    void startHoldingBall()
    {
        bowlingBall.setHeld(true);
        ballHeld = true;
    }

    
}
