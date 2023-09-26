using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ٸ� ���� �з��� ���� ������ ���ٸ�, �ܼ��� ���μ��� ��ɸ� �����ϴ� ���������� ����Ѵ�.
/// </summary>
[CreateAssetMenu(fileName = "ItemData", menuName = "ItemInfoController/Default", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("Item Data")]
    public string itemName;
    public string lable;
    public Sprite itemImage;
}
