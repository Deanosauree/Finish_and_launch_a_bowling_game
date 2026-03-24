using System;
using UnityEngine;
using UnityEngine.Events;

public class buttonRelay : MonoBehaviour
{
    [SerializeField] int buttonIndex;


    public IntEvent buttonPressed;

    [Serializable]
    public class IntEvent : UnityEvent<int> { }
    public void OnPress()
    {
        buttonPressed.Invoke(buttonIndex);
    }

}
