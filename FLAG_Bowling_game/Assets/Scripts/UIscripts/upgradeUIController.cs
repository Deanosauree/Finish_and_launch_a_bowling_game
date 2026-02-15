using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class upgradeUIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    GameObject[] buttons;
    TextMeshProUGUI[] buttonTextMeshes;
    [SerializeField] GameObject panelObject;

    public UnityEvent<int> upgradePressed;
    void Start()
    {
        buttonTextMeshes = new TextMeshProUGUI[3];
        int thisButton = 0;
        foreach (var button in buttons)
        {
            buttonTextMeshes[thisButton] = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonRelay relay = button.GetComponent<buttonRelay>();
            relay.buttonPressed.AddListener(IndexedButtonPress);
        }
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

    public void IndexedButtonPress(int index)
    {
        //upgradePressed.Invoke(index);
        Debug.Log(index);
    }
}
