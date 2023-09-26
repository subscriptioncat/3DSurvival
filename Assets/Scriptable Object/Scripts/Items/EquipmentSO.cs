using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비품. 방어력, 공격력 등에 영향을 준다. 전투 관련 스텟과 장비를 구현하지 않는다면 삭제할 예정.
/// </summary>
[CreateAssetMenu(fileName = "EquipmentData", menuName = "ItemInfoController/EquipmentInfo", order = 1)]
public class EquipmentSO : ItemSO
{
    [Header("Equipment Data")]
    public int atk;
    public int def;
    public bool isEquiped;
}
