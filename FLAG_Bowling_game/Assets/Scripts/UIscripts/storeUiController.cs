using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class storeUiController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    GameObject[] buttons;
    TextMeshProUGUI[] buttonTextMeshes;
    [SerializeField] GameObject panelObject;

    public UnityEvent<int> upgradePressed;
    public UnityEvent<int> upgradeHovered;
    public int buttonCount = 0;

    private void Awake()
    {
        buttonTextMeshes = new TextMeshProUGUI[] { buttons[0].GetComponentInChildren<TextMeshProUGUI>(), buttons[1].GetComponentInChildren<TextMeshProUGUI>(), buttons[2].GetComponentInChildren<TextMeshProUGUI>() };
        foreach (var button in buttons) 
        {
            buttonRelay relay = button.GetComponent<buttonRelay>();
            relay.buttonPressed.AddListener(IndexedButtonPress);
            buttonCount += 1;
        }
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
    }

    public void SetUpgradeNames(string upgradeOne, string upgradeTwo, string upgradeThree)
    {
        buttonTextMeshes[0].SetText(upgradeOne);
        buttonTextMeshes[1].SetText(upgradeTwo);
        buttonTextMeshes[2].SetText(upgradeThree);

    }

    public void indexedButtonHover(int index)
    {
        upgradeHovered.Invoke(index);
    }

    public void IndexedButtonPress(int index)
    {
        upgradePressed.Invoke(index);
    }
}
