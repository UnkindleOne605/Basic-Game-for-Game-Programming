using UnityEngine;
using System.Collections.Generic;

public class LootBag : MonoBehaviour
{
    [System.Serializable]
    public class LootItem
    {
        public Items item;
        [Range(1, 101)]
        public int dropChance;
    }

    public List<LootItem> lootItems = new List<LootItem>();
    public GameObject lootPrefab;

    Items? GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);

        foreach (LootItem lootItem in lootItems)
        {
            if (randomNumber <= lootItem.dropChance)
            {
                return lootItem.item;
            }
        }

        Debug.Log("No item dropped.");
        return null;
    }
    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Items? droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootObject = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
            ItemPickup itemPickup = lootObject.GetComponent<ItemPickup>();

            if (itemPickup != null)
            {
                itemPickup.itemDrop = droppedItem.Value;
            }

        }

    }

}

