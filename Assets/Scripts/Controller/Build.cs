using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_BuildingName
{
    Tent,
}

public class Build : MonoBehaviour
{
    //모든 건축 리스트를 저장해야 됩니다.
    [SerializeField] private List<BuildSO> buildList;
    public List<BuildSO> BuildList { get { return buildList; } }

    [Range(0, 20)][SerializeField] private int previewDistance;

    private int nowIndex;
    private GameObject gameobject;
    private Transform objectTransform;

    public bool IsBuildingMaterials(int selectIndex)
    {
        nowIndex = buildList[selectIndex].ConsumingResources.Count;
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
    void DisplayExample()
    {
        gameobject = Instantiate(buildList[nowIndex].Prefab, transform.position + transform.forward * previewDistance, Quaternion.identity);
        objectTransform = gameobject.transform;
        var objectMeashCollider = gameobject.GetComponent<MeshCollider>();
        objectMeashCollider.convex = true;
        objectMeashCollider.isTrigger = true;
    }

    private void Start()
    {
        DisplayExample();
    }
    private void Update()
    {
        objectTransform.position = transform.position + transform.forward * previewDistance;
        objectTransform.rotation = transform.rotation;
    }
    //public bool IsCollision()
    //{

    //}
    //public void Build()
    //{
    //    //
    //}
}
