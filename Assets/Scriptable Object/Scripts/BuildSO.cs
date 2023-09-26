using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildSO", menuName = "Build/BuildSO", order = int.MaxValue)]
public class BuildSO :ScriptableObject
{
    [SerializeField] private string buildingname;
    public string BuildingName { get { return buildingname; } }
    [SerializeField] private string explanation;
    public string Explanation { get { return explanation; } }
    [SerializeField] private int maxHp;
    public int MaxHp { get { return maxHp; } }
    [SerializeField] private LayerMask layer;
    public LayerMask Layer { get { return layer;} }

    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get { return prefab; } }

    [System.Serializable]
    public struct MyDictionary
    {
        public string itemName;
        public int numberConsumed;
    }

    [SerializeField] private List<MyDictionary> consumingResources;
    public List<MyDictionary> ConsumingResources { get { return consumingResources; } }
}