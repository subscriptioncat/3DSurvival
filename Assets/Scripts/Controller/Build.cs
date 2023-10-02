using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuildSO;

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

    [Range(0f, 100f)][SerializeField] private int MaxPosX;
    [Range(0f, -100f)][SerializeField] private int MinPosX;
    [Range(0f, 100f)][SerializeField] private int MaxPosY;
    [Range(0f, -100f)][SerializeField] private int MinPosY;

    private int selectIndex;
    private GameObject preViewGameObject;
    private Transform preViewGameObjectTransform;
    private PreviewObject preViewComponent;
    private bool isPreViewActive = false;

    //팝업창에서 이름 및 소모하는 재료를 보여주기위해 buildList를 모두 전달
    public List<BuildSO> GetConsumingResources()
    {
        return buildList;
    }

    //팝업창에서 선택하면 호출
    public void OnPreView(int _selectIndex)
    {
        if (IsBuildingMaterials(_selectIndex))
        {
            DisplayPreView();
        }
        else
        {
            //재료가 부족합니다.
        }
    }

    //마우스 좌클릭하면 호출
    public void OnBuild()
    {
        if (isPreViewActive) 
        {

        }
    }

    //재료가 있는지 확인
    public bool IsBuildingMaterials(int _selectIndex)
    {
        selectIndex = _selectIndex;
        int maxIndex = buildList[selectIndex].ConsumingResources.Count;
        for (int i = 0; i < maxIndex; i++)
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

    //PreView를 표시합니다.
    void DisplayPreView()
    {
        isPreViewActive = true;
        preViewGameObject = Instantiate(buildList[selectIndex].PreViewPrefab, transform.position + transform.forward * previewDistance, Quaternion.identity);
        preViewGameObjectTransform = preViewGameObject.transform;
        preViewComponent = preViewGameObject.GetComponent<PreviewObject>();
    }
    //건축물을 생성합니다.
    private void InstantiateBuilding()
    {
        Instantiate(buildList[selectIndex].Prefab, transform.position + transform.forward * previewDistance, Quaternion.identity);
    }
}
