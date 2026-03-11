using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public double currentScore;
    public pinChance[] pinChanceArray;

    private void OnEnable()
    {
        
    }
}

[System.Serializable]
public class pinChance
{
    [SerializeField] string pinName;
    [SerializeField] float chance;
}
