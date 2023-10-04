using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    string IInteractable.GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.itemName);
    }

    void IInteractable.OnPickUp()
    {
        InventoryManager.instance.AddItem(item);
    }
}
