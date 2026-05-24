using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class pinStoreManager : MonoBehaviour
{
    [SerializeField] private storeUiController uiController;
    [SerializeField] private PinSpawner spawner;
    [SerializeField] PinCostAttributes[] pinAttributes;

    [SerializeField] LocalizedString points;

    private int[] timesPinsPurchased;
    private int[] timesThesePurchased;
    private int[] chosenPins;
    public float currentPoints;
    private System.Random rand = new System.Random();
    private AudioSource auSource;

    private void Awake()
    {
        timesPinsPurchased = new int[pinAttributes.Length];
        chosenPins = new int[uiController.buttonCount];
        timesThesePurchased = new int[uiController.buttonCount];
        uiController.upgradePressed.AddListener(cardPressed);
        uiController.upgradeHovered.AddListener(cardHovered);
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        auSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        uiController.updatePointsText(points.GetLocalizedString());
    }

    public void setShopOpen(bool open)
    {
        if (open)
        {
            pickRandomUpgrades();
            setupUI();
        }
        else
        {
            uiController.SetUpgradesVisible(false);
        }
    }

    public Dictionary<string,float> getWeights()
    {
        Dictionary<string, float> weights = new Dictionary<string, float>();
        for (int i = 0; i < pinAttributes.Length; i++)
        {
            PinCostAttributes attr = pinAttributes[i];
            float weight = attr.baseProbability + attr.pinProbabilityIncrease*timesPinsPurchased[i];
            string name = attr.pinKey;
            weights.Add(name, weight);
        }
        return weights;
    }

    private void setupUI()
    {
        List<PinCostAttributes> chosenPinAttributes = new List<PinCostAttributes>();
        foreach (int chosen in chosenPins)
        {
            chosenPinAttributes.Add(pinAttributes[chosen]);

        }
        PinCostAttributes[] chosenAttArr = chosenPinAttributes.ToArray();
        uiController.SetUpgrades(chosenAttArr, timesThesePurchased);
        uiController.SetUpgradesVisible(true);
    }

    private void pickRandomUpgrades()
    {
        List<float> chances = new List<float>();
        float totalProbability = 0;
        foreach (PinCostAttributes thisPin in pinAttributes)
        {
            chances.Add(thisPin.pinChance);
            totalProbability += thisPin.pinChance;
        }
        for (int i = 0; i < chosenPins.Length; i++)
        {
            float pinChosen = Random.Range(0, totalProbability);
            float currentChance = 0;
            int currentPin = 0;
            foreach (float chance in chances)
            {
                currentChance += chance;
                if (pinChosen < currentChance)
                {
                    break;
                }
                currentPin += 1;
            }
            chosenPins[i] = currentPin;
            timesThesePurchased[i] = timesPinsPurchased[currentPin];
            totalProbability -= chances[currentPin];
            chances[currentPin] = 0;
        }
    }

    void OnLocaleChanged(Locale newLocale)
    {
        uiController.updatePointsText(points.GetLocalizedString());
    }

    public void showUpgrades()
    {

    }

    

    public void cardHovered(int index)
    {
        int correctedIndex = chosenPins[index];
        PinCostAttributes pin = pinAttributes[correctedIndex];
        LocalizedString pinName = pin.pinName;
        LocalizedString pinDesc = pin.description;
        uiController.updateDesrciption(pinName.GetLocalizedString(), pinDesc.GetLocalizedString());
    }

    public void cardPressed(int index)
    {
        int correctedIndex = chosenPins[index];
        PinCostAttributes pin = pinAttributes[correctedIndex];
        float cost = pin.pinPrice * Mathf.Pow(pin.pinPriceMultiplier, timesPinsPurchased[correctedIndex]);
        if (cost <= currentPoints)
        {
            currentPoints -= cost;
            timesPinsPurchased[correctedIndex]++;
            uiController.updateUpgrade(pin, index, timesPinsPurchased[correctedIndex]);
            uiController.updatePoints(currentPoints);
            auSource.Play();
        }
        
    }

    public void addPoints(float points)
    {
        currentPoints += points;
        uiController.updatePoints(currentPoints);
    }
}
