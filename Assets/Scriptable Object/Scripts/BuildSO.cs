using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_BuildingType
{
    Accommodation,
    Interaction,
    NotInteraction
}

[System.Serializable]
public class MyDictionary
{
    public ItemData itemData;
    public int numberConsumed;
}

[CreateAssetMenu(fileName = "BuildSO", menuName = "Build/BuildSO", order = int.MaxValue)]
public class BuildSO :ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string buildingName;
    [SerializeField] private string explanation;
    [SerializeField] private int maxHp;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject prefab;
    [SerializeField] private E_BuildingType buildingType;

    [Header("Resource Item")]
    [SerializeField] private MyDictionary[] consumingResources;
    

    public string BuildingName { get { return buildingName; } }
    public string Explanation { get { return explanation; } }
    public int MaxHp { get { return maxHp; } }
    public LayerMask Layer { get { return layer; } }
    public GameObject Prefab { get { return prefab; } }
    public E_BuildingType BuildingType { get { return buildingType; } }
    public MyDictionary[] ConsumingResources{ get { return consumingResources; } }
}