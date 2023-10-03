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

    public string ItemName { get { return item.itemName; } }
    public string Label { get { return item.lable; } }
    public Sprite Icon { get { return item.icon; } }
    public int Amount { get { return amount; } }
    public CraftMaterial[] Materials{ get { return materials; } }
}