using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 다른 하위 분류에 대한 정보가 없다면, 단순히 재료로서의 기능만 수행하는 아이템으로 취급한다.
/// </summary>

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
    public bool isStacable;
    public int maxStackAmount;
}
