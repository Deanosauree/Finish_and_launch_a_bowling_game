using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class storeUiController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    GameObject[] buttons;
    TextMeshProUGUI[] buttonTextMeshes;
    [SerializeField] GameObject panelObject;
    [SerializeField] TextMeshProUGUI[] descriptionPanel;
    [SerializeField] TextMeshProUGUI pointsNumber;
    [SerializeField] TextMeshProUGUI pointsText;

    public UnityEvent<int> upgradePressed;
    public UnityEvent<int> upgradeHovered;
    [HideInInspector] public int buttonCount = 0;

    private void Awake()
    {
        buttonTextMeshes = new TextMeshProUGUI[] 
        { buttons[0].GetComponentInChildren<TextMeshProUGUI>(), buttons[1].GetComponentInChildren<TextMeshProUGUI>(), 
            buttons[2].GetComponentInChildren<TextMeshProUGUI>(), buttons[3].GetComponentInChildren<TextMeshProUGUI>(), 
            buttons[4].GetComponentInChildren<TextMeshProUGUI>() };
        buttonCount = 0;
        foreach (var button in buttons) 
        {
            buttonRelay relay = button.GetComponent<buttonRelay>();
            relay.buttonPressed.AddListener(IndexedButtonPress);
            buttonCount += 1;
        }
        
        SetUpgradesVisible(false);

    }
        
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpgradesVisible(bool visible)
    {
        panelObject.SetActive(visible);
        foreach (var button in buttons) { button.SetActive(visible); }
    }

    public void SetUpgrades(PinCostAttributes[] pins, int[] timesPurchased)
    {
        for (int i = 0; i < pins.Length; i++) 
        {
            PinCostAttributes pin = pins[i];
            float cost = pin.pinPrice * Mathf.Pow(pin.pinPriceMultiplier, timesPurchased[i]);
            buttonTextMeshes[i].text = cost.ToString("0.");
            buttons[i].GetComponent<Image>().sprite = pin.card;
        }

    }

    public void updateUpgrade(PinCostAttributes pin, int index, int timesPurchased)
    {
        float cost = pin.pinPrice * Mathf.Pow(pin.pinPriceMultiplier, timesPurchased);
        buttonTextMeshes[index].text = cost.ToString();
    }
    private void hideUpgrade(int index)
    {
        buttons[index].SetActive(false);
    }

    public void updateDesrciption(string name, string description)
    {
        descriptionPanel[0].text = name;
        descriptionPanel[1].text = description;
    }

    public void indexedButtonHover(int index)
    {
        upgradeHovered.Invoke(index);
    }

    public void IndexedButtonPress(int index)
    {
        upgradePressed.Invoke(index);
    }

    public void updatePoints(float points)
    {
        pointsNumber.text = points.ToString("0000000.00");
    }

    public void updatePointsText(string text)
    {
        pointsText.text = text;
    }
}
