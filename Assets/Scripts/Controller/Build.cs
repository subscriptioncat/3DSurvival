using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_BuildingName
{
    Tent,
}

public class Build : MonoBehaviour
{
    //��� ���� ����Ʈ�� �����ؾ� �˴ϴ�.
    [SerializeField] private List<BuildSO> buildList;
    public List<BuildSO> BuildList { get { return buildList; } }

    public bool IsBuildingMaterials(int selectIndex)
    {
        int nowIndex = buildList[selectIndex].ConsumingResources.Count;
        for (int i = 0; i < nowIndex; i++)
        {
            //if(!Inventory.instance.TryConsume(
            //    buildList[selectIndex].ConsumingResources[i].itemName,
            //    buildList[selectIndex].ConsumingResources[i].numberConsumed
            //    ))
            //{
            //    return false;
            //}
        }
        return true;
    }
    //public bool IsCollision()
    //{

    //}
    //public void Build()
    //{
    //    //
    //}
}
