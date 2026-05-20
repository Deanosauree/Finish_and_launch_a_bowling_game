using UnityEngine;
using UnityEngine.Events;

public delegate void Notify();

public static class PointCalc
{
    private static int pinsDropped = 0;
    private static float totalPoints = 0;
    private static float multyplier = 1.0f;

    public static bool evenRound = false;

    public static event Notify PinDroppedEvent;




    public static void PinDropped(float points, float mult)
    {
        pinsDropped ++;
        totalPoints = totalPoints + points;
        multyplier = multyplier * mult;


        SetPointLabel(totalPoints);
        SetMulLabel(multyplier);
        PinDroppedEvent.Invoke();
    }

    public static void addMultiplier(float multi)
    {
        multyplier += multi;
        SetPointLabel(totalPoints);
        SetMulLabel(multyplier);
    }

    public static (float, float, int) getPointsMultiPins()
    {
        return (totalPoints, multyplier, pinsDropped);
    }

    public static void SetPointLabel(double value)
    {
        string labelValue = FormatNumbers.Format(value);

        Debug.Log("points: ");
    }

    public static void SetMulLabel(double value)
    {
        string labelValue = FormatNumbers.Format(value);
        Debug.Log("multyplier: ");
    }

}
