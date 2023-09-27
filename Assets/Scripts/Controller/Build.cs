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
    private GameObject preViewGameObject;
    private Transform preViewGameObjectTransform;
    private PreviewObject preViewComponent;
    private bool isPreViewActive = false;

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
        isPreViewActive = true;
        preViewGameObject = Instantiate(buildList[nowIndex].PreViewPrefab, transform.position + transform.forward * previewDistance, Quaternion.identity);
        preViewGameObjectTransform = preViewGameObject.transform;
        preViewComponent = preViewGameObject.GetComponent<PreviewObject>();
    }
    private void InstantiateBuilding()
    {
        Instantiate(buildList[nowIndex].Prefab, transform.position + transform.forward * previewDistance, Quaternion.identity);
    }

    //테스트를 위한 코드~
    private void Start()
    {
        DisplayExample();
    }
    private void Update()
    {
        if (isPreViewActive)
        {
            preViewGameObjectTransform.position = transform.position + transform.forward * previewDistance;
            preViewGameObjectTransform.rotation = transform.rotation;
        }
        
    }
    //까지


    public void OnPreView()
    {
        if (IsBuildingMaterials(0))
        {
            DisplayExample();
        }
        else
        {
            //자재가 부족합니다.
        }
    }

    public void OnBuild()
    {
        if (isPreViewActive)
        {
            if (IsBuildingMaterials(0))
            {
                if (preViewComponent.isBuildable())
                {
                    InstantiateBuilding();
                }
            }
            else
            {
                OnExitPreView();
                //자재가 부족합니다.
            }
        }
    }
    public void OnExitPreView()
    {
        isPreViewActive = false;
        Destroy(preViewGameObject);
    }
}
