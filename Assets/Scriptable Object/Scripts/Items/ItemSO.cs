using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 다른 하위 분류에 대한 정보가 없다면, 단순히 재료로서의 기능만 수행하는 아이템으로 취급한다.
/// </summary>
[CreateAssetMenu(fileName = "ItemData", menuName = "ItemInfoController/Default", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("Item Data")]
    public string itemName;
    public string lable;
    public Sprite itemImage;
}
