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
        pinStoreManager.setShopOpen(true);
    }

    public void setBallType(GameObject ballPrefab)
    {
        ballManager.setBallType(ballPrefab);
    }

    public void bowlingStarted()
    {
        pinStoreManager.setShopOpen(false);
    }

    public void pinsReset()
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
    }

}
