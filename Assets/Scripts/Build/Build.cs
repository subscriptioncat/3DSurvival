using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingsList
{
    public BuildSO building;
    public Vector3 pos;
    public Vector3 rotation;
}

public class Build : MonoBehaviour, IInteractable
{
    //모든 건축 리스트를 저장해야 됩니다.
    [SerializeField] private BuildingsList[] buildingsList;
    public BuildingsList[] BuildingsList { get { return buildingsList; } }
    [SerializeField] private Material green;
    [SerializeField] private Canvas canvas;
    public Canvas Canvas { get { return canvas; } }

    private bool isPreViewActive = false;
    private List<GameObject> previewList;

    private void Start()
    {
        PreViewInstantiate();
    }

    public void PreViewInstantiate()
    {
        previewList = new List<GameObject>();
        foreach (var building in buildingsList)
        {
            var nowObject = Instantiate(building.building.Prefab, building.pos + transform.position, Quaternion.Euler(building.rotation));
            nowObject.GetComponent<BuildingObject>().GetMyBuild(this);
            int count = nowObject.GetComponentInChildren<Renderer>().materials.Length;
            Material[] newMaterials = new Material[count];
            for (int i = 0; i < count; i++)
            {
                newMaterials[i] = green;
            }
            nowObject.GetComponentInChildren<Renderer>().materials = newMaterials;
            previewList.Add(nowObject);
        }
    }

    public string GetInteractPrompt()
    {
        if (isPreViewActive)
        {
            return string.Format("Hide PreView");
        }
        else
        {
            return string.Format("DisPlyer PreView");
        }
    }

    public void OnPickUp()
    {
        if(isPreViewActive)
        {
            OnHide();
        }
        else
        {
            OnDisplay();
        }
        
    }

    public bool RemovePreview(GameObject gameObject) 
    {
        return previewList.Remove(gameObject);
    }

    public void OnDisplay()
    {
        isPreViewActive = true;
        foreach (var gameObject in previewList)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnHide()
    {
        isPreViewActive = false;
        foreach (var gameObject in previewList)
        {
            gameObject.SetActive(false);
        }
    }
}
