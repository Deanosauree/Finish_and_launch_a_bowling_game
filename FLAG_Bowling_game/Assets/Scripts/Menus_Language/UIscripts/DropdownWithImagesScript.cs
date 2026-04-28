using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownWithImagesScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    TMP_Dropdown dropdown;
    [SerializeField] GameObject selectedBall;
    public List<OptionData> options;
    [SerializeField] Image image;
    

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        loadOptions();
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        if (selectedBall != null)
        {
            PlayerInfo.bowlingBall = selectedBall;
        }

    }

    [Serializable]
    public class OptionData
    {
        [SerializeField]
        private LocalizedString m_Text;
        [SerializeField]
        private Sprite m_Image;
        [SerializeField]
        private GameObject m_ball;

        public GameObject ball { get { return m_ball; } }

        public LocalizedString text { get { return m_Text; } }

        public Sprite image { get { return m_Image; } }
        public OptionData() { }

    }
    /// <summary>
    /// Class used internally to store the list of options for the dropdown list.
    /// </summary>
    /// <remarks>
    /// The usage of this class is not exposed in the runtime API. It's only relevant for the PropertyDrawer drawing the list of options.
    /// </remarks>
    
    void OnLocaleChanged(Locale newLocale)
    {
        loadOptions();
    }
    void loadOptions()
    {
        List<string> optionsText = new List<string>();
        foreach (OptionData optionData in options) 
        {
            optionsText.Add(optionData.text.GetLocalizedString());
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(optionsText);
        selectedBall = options[0].ball;
        image.sprite = options[0].image;
    }

    public void ballSelected(int index)
    {
        selectedBall = options[index].ball;
        image.sprite = options[index].image;
        PlayerInfo.bowlingBall = selectedBall;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
