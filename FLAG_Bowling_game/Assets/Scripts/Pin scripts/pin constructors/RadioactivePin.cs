using UnityEngine;

public class RadioactivePin : PinBase
{
    public RadioactivePin()//constractor
    {
        points = 50;
        weight = 1;
        GMultiplier = 1.05f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            PinBase pinScript = other.GetComponent<PinBase>();
            if (pinScript != null) 
            {
                if (!pinScript.isMultAdded) 
                { 
                    pinScript.GMultiplier *= GMultiplier;
                }
            }
        }
    }
}
