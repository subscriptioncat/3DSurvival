using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[System.Serializable]
public class BuildingsList
{
    public BuildSO building;
    public Vector3 pos;
}

public class Build : MonoBehaviour
{
    //모든 건축 리스트를 저장해야 됩니다.
    [SerializeField] private BuildingsList[] buildingsList;
    public BuildingsList[] BuildingsList { get { return buildingsList; } }
    [SerializeField]
    private Material green;

    private int selectIndex;
    private GameObject preViewGameObject;
    private Transform preViewGameObjectTransform;
    private PreviewObject preViewComponent;
    private bool isPreViewActive = false;

    private void Start()
    {
        PreViewInstantiate();
    }

    public void PreViewInstantiate()
    {
        foreach (var building in buildingsList)
        {
            var nowObject = Instantiate(building.building.Prefab, building.pos + transform.position, Quaternion.identity);
            int count = nowObject.GetComponentInChildren<Renderer>().materials.Length;
            Material[] newMaterials = new Material[count];
            for (int i = 0; i < count; i++)
            {
                newMaterials[i] = green;
            }
            nowObject.GetComponentInChildren<Renderer>().materials = newMaterials;
        }
    }


}
