using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 식료품. 사용 시 허기, 갈증, 체력, 스테미너 등에 영향을 준다.
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
