using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable,
}

public enum ConsumableType
{
    Hunger,
    Thirst,
    Health,
    Stemina,
    Heat
}

[System.Serializable]
public class ConsumableData
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string lable;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool isStackable;
    public int maxStackAmount;

    [Header("Consumable")]
    public ConsumableData[] consumables;
}
