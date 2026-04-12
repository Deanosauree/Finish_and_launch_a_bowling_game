using UnityEngine;

public class ballStoreMenu : MonoBehaviour
{
    GameObject panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        panel = this.gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive(bool active)
    {
        panel.SetActive(active);
    }
}
