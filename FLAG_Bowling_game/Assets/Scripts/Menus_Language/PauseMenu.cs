using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    PlayerInputs playerInputs;
    void Awake()
    {
        playerInputs = new PlayerInputs();
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    void Update()
    {
        if (playerInputs.bowlingControls.Escape.ReadValue<float>() > 0.1f)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
