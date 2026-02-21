using UnityEditor.SceneManagement;
using UnityEngine;

public class ballManager: MonoBehaviour
{
    [SerializeField] Transform ballHoldLocation;
    [SerializeField] upgradeUIController upgradeUI;
    [SerializeField] Transform[] ballDisplayPositions;

    [SerializeField] GameObject BowlingBallPrefab;
    [SerializeField] float throwPower = 1;

    private bowlingBallBase bowlingBall;
    private bool ballHeld = false;
    private bowlingBallBase[] ballChoices;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeUI.upgradePressed.AddListener(upgradeSelected);
        bowlingBall = Instantiate(BowlingBallPrefab, ballHoldLocation).GetComponent<bowlingBallBase>();
        bowlingBall.enabled = true;
        startHoldingBall();

    }

    // Update is called once per frame
    void Update()
    {
        if (ballHeld)
        {
            bowlingBall.setLocation(ballHoldLocation.position, ballHoldLocation.rotation);
        }
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

    void upgradeSelected(int index)
    {

    }

    void startHoldingBall()
    {
        bowlingBall.setHeld(true);
        ballHeld = true;
    }

    
}
