using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftMaterial
{
    public ItemData itemData;
    public int amount;
}

[CreateAssetMenu(fileName = "CraftSO", menuName = "CraftRecipe")]
public class CraftSO:ScriptableObject
{
    [Header("Info")]
    [SerializeField] private ItemData item;
    [SerializeField] private int amount;

    [Header("Resource Item")]
    [SerializeField] private CraftMaterial[] materials;

    public ItemData Item { get { return item; } }
    public int Amount { get { return amount; } }
    public CraftMaterial[] Materials{ get { return materials; } }
}