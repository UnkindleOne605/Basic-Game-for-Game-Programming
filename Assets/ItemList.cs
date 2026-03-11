using UnityEngine;

[System.Serializable]
public class ItemList
{
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
