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
            text += myDictionary.itemName;
            int count = myDictionary.itemName.Length;
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
        gameObject.GetComponentInChildren<Renderer>().materials = defaultMat;
        gameObject.layer = 10;
        gameObject.GetComponent<MeshCollider>().isTrigger = false;
        gameObject.GetComponent<MeshCollider>().convex = false;
        myBuild.RemovePreview(this.gameObject);
    }

    public void GetMyBuild(Build build)
    {
        myBuild = build;
    }

}
