using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 식료품 등의 소모성 아이템이다. 해당 아이템이 포함되는 하위 분류에 대한 정보는 E_Tag 로 나타낸다.
/// </summary>
[CreateAssetMenu(fileName = "UsableData", menuName = "ItemInfoController/Usable/Default", order = 0)]
public class UsableSO : ItemSO
{
    [Header("Usable Data")]
    public E_UsableTag tag;
}

public enum E_UsableTag
{
    EIDIBLE,
}
