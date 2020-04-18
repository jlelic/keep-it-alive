using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Item> grabbedItems = new List<Item>();

    public void GrabItem(Item item)
    {
        grabbedItems.Add(item);
        Destroy(item.GetComponent<Rigidbody2D>());
        item.transform.parent = transform;
    }

    public void AddItemsToInventory()
    {
        foreach(var item in grabbedItems)
        {
            item.SpawnInventoryPrefab();
            Destroy(item.gameObject);
        }
        grabbedItems.Clear();
    }
}
