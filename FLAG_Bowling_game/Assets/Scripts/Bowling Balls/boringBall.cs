using UnityEngine;

public class boringBall : bowlingBallBase
{
    public boringBall(float points=50, float additiveMultiplier=1, float mulMultiplier=1) : base(points, additiveMultiplier, mulMultiplier)
    {
        this.points += points;
        this.additiveMultiplier += additiveMultiplier;
        this.mulMultiplier += mulMultiplier;
    }

}
