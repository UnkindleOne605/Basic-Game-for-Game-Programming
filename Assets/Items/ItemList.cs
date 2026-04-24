using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemList
{
    [SerializeReference]
    public Item item;
    public string name;
    public int amount;

    public ItemList(Item newItem, string newName, int newAmount)
    {
        item = newItem;
        name = newName;
        amount = newAmount;
    }
}
