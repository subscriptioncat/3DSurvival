using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ķ�ǰ. ��� �� ���, ����, ü��, ���׹̳� � ������ �ش�.
/// </summary>
[CreateAssetMenu(fileName = "EdibleData", menuName = "ItemInfoController/Usable/EdibleInfo", order = 1)]
public class EdibleSO : UsableSO
{
    [Header("Edible Data")]
    public int foodPoint;
    public int waterPoint;
    public int steminaPoint;
    public int HealthPoint;
}
