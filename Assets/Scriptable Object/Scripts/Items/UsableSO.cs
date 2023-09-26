using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ķ�ǰ ���� �Ҹ� �������̴�. �ش� �������� ���ԵǴ� ���� �з��� ���� ������ E_Tag �� ��Ÿ����.
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
