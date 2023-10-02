using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour, IInteractable
{
    public BuildSO building;
    [SerializeField]
    private Material[] defaultMat;

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

    public void OnInteract()
    {
        gameObject.GetComponentInChildren<Renderer>().materials = defaultMat;
        gameObject.GetComponent<MeshCollider>().isTrigger = false;
        gameObject.GetComponent<MeshCollider>().convex = false;
    }

}
