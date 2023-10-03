using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour, IInteractable
{
    public BuildSO building;
    [SerializeField]
    private Material[] defaultMat;

    private Build myBuild;

    public string GetInteractPrompt()
    {
        string text = "";
        foreach (var myDictionary in building.ConsumingResources)
        {
            text += myDictionary.itemData.itemName;
            int count = myDictionary.itemData.itemName.Length;
            for (int i = 0; i < 10 - count; i++)
            {
                text += " ";
            }
            text += "X " + myDictionary.numberConsumed + "\n";
        }
        return string.Format("Build {0}\n{1}", building.BuildingName, text);
    }

    public void OnPickUp()
    {
        if (CheckItem())
        {
            Debug.Log("성공");
            //ResourceConsumption();
            gameObject.GetComponentInChildren<Renderer>().materials = defaultMat; // 원색으로 변경
            gameObject.layer = 10; //상호작용 해제
            gameObject.GetComponent<MeshCollider>().isTrigger = false; //충돌설정
            gameObject.GetComponent<MeshCollider>().convex = false;
            myBuild.RemovePreview(this.gameObject); //프리뷰 리스트에서 해제
        }
        else
        {
            Debug.Log("실패");
            //실패팝업 띄우기
        }
    }

    public void GetMyBuild(Build build)
    {
        myBuild = build;
    }

    public bool CheckItem()
    {
        foreach (var myDictionary in building.ConsumingResources)
        {
            if(!InventoryManager.instance.IsEnough(myDictionary.itemData, myDictionary.numberConsumed))
            {
                return false;
            }
            
        }
        return true;
    }

    public void ResourceConsumption()
    {
        foreach (var myDictionary in building.ConsumingResources)
        {
            InventoryManager.instance.RemoveMaterials(myDictionary.itemData, myDictionary.numberConsumed);
        }
        
    }

}
