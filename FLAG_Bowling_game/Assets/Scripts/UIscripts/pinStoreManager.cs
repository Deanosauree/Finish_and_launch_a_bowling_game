using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class pinStoreManager : MonoBehaviour
{
    [SerializeField] private storeUiController uiController;
    [SerializeField] private PinSpawner spawner;
    [SerializeField] PinCostAttributes[] pinAttributes;


    private int[] chosenPins;
    private System.Random rand = new System.Random();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        chosenPins = new int[uiController.buttonCount];
        uiController.upgradePressed.AddListener(cardPressed);
        uiController.upgradeHovered.AddListener(cardHovered);
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }
    void Start()
    {
        pickRandomUpgrades();
        setupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setupUI()
    {
        List<PinCostAttributes> chosenPinAttributes = new List<PinCostAttributes>();
        foreach (int chosen in chosenPins)
        {
            chosenPinAttributes.Add(pinAttributes[chosen]);
        }
        PinCostAttributes[] chosenAttArr = chosenPinAttributes.ToArray();
        uiController.SetUpgrades(chosenAttArr);
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
            totalProbability -= chances[currentPin];
            chances[currentPin] = 0;
        }
    }

    void OnLocaleChanged(Locale newLocale)
    {

    }

    public void showUpgrades()
    {

    }

    

    public void cardHovered(int index)
    {
        Debug.Log(index);
        int correctedIndex = chosenPins[index];
        PinCostAttributes pin = pinAttributes[correctedIndex];
        LocalizedString pinName = pin.pinName;
        LocalizedString pinDesc = pin.description;
        uiController.updateDesrciption(pinName.GetLocalizedString(), pinDesc.GetLocalizedString());
    }

    public void cardPressed(int index)
    {

    }
}
