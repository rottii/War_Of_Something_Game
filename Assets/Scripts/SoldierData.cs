using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradableUnitStats
{
    //health, speed, damage etc.
    public string statID;
    public string displayName;
    public float baseValue;

    public float statIncreasePerLevel;
}

[CreateAssetMenu(fileName = "NewSoldier", menuName = "Soldier/Soldier Data")]
public class SoldierData : ScriptableObject
{
    public string soldierName;
    public Sprite icon;       // Image to show on the selection line
    public GameObject prefab; // The actual soldier to spawn
    public float timeToSpawn;

    public List<UpgradableUnitStats> unitStats;
}