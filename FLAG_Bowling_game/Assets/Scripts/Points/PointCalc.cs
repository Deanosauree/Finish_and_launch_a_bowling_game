using UnityEngine;

public static class PointCalc
{
    public static int pinsDropped = 0;
    public static float totalPoints = 0;
    public static float multyplier = 1.0f;




    public static void PinDropped(float points, float mult)
    {
        pinsDropped += 1;
        totalPoints += points;
        multyplier *= mult;

        Debug.Log("pin: " + pinsDropped);
        Debug.Log("points: " + totalPoints);
        Debug.Log("multiplier: " + multyplier);
    }
}
