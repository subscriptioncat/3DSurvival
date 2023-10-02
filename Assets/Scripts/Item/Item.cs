using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// æ∆¿Ã≈€ id, ItemSO, 
/// </summary>
public class Item : MonoBehaviour
{
    private ItemSO itemSO;
    private int quantity;

    public Item(ItemSO item, int quant)
    {
        ItemSO = item;
        quantity = quant;
    }

    public ItemSO ItemSO
    {
        get { return itemSO; }
        set
        {
            itemSO = value;
        }
    }

    public int Quantity
    {
        get { return quantity; }
        set
        {
            quantity = value;
        }
    }
}
