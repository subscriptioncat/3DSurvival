using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ǰ. ����, ���ݷ� � ������ �ش�. ���� ���� ���ݰ� ��� �������� �ʴ´ٸ� ������ ����.
/// </summary>
[CreateAssetMenu(fileName = "EquipmentData", menuName = "ItemInfoController/EquipmentInfo", order = 1)]
public class EquipmentSO : ItemSO
{
    [Header("Equipment Data")]
    public int atk;
    public int def;
    public bool isEquiped;
}
