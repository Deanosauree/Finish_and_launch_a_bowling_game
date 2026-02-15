using UnityEngine;

public class boringBall : bowlingBallBase
{
    public boringBall(float points, float additiveMultiplier, float mulMultiplier) : base()
    {
        this.points = 50 + points;
        this.additiveMultiplier = 1.0f + additiveMultiplier;
        this.mulMultiplier = 1.0f + mulMultiplier;
    }

}
