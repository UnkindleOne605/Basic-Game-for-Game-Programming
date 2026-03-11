using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.Search;
using Unity.VisualScripting;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public Items itemDrop;

    void Start()
    {
        item = AssignItem(itemDrop);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player picked up: " + item.GiveName());
            PlayerStats player = other.GetComponent<PlayerStats>();
            AddItem(player);
            Destroy(this.gameObject);
        }

        Debug.Log("Triggered by: " + other.name);
    }

    public Item AssignItem(Items item)
    {
        switch (item)
        {
            case Items.RegenItem:
                return new RegenItem();
            case Items.Stake:
                return new Stake();
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
    RegenItem,
    Stake
}