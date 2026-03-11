using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.Search;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public Items itemDrop;

    void Start()
    {
        item = AssignItem(itemDrop);
    }

    public Item AssignItem(Items item)
    {
        switch (item)
        {
            case Items.RegenItem:
                return new RegenItem();
            default:
                return new RegenItem();
        }
    }

    public void AddItem(PlayerStats player)
    {
        foreach(ItemList i in player.items)
        {
            if (i.name == item.GiveName())
            {
                i.amount += 1;
                return;
            }
        }
        player.items.Add(new ItemList(item, item.GiveName(), 1));
    }
}


public enum Items
{
    RegenItem
}