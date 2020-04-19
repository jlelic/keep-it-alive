using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Item> grabbedItems = new List<Item>();

    public void GrabItem(Item item)
    {
        Debug.Log(item.gameObject);
        grabbedItems.Add(item);
//        Destroy(item.GetComponent<Rigidbody2D>());
        item.transform.parent = transform;
    }

    public void AddItemsToInventory()
    {
        for (int i = 0; i < grabbedItems.Count; i++)
        { 
            var item = grabbedItems[i];
            item.SpawnInventoryPrefab(i);
            Destroy(item.gameObject);
        }
        grabbedItems.Clear();
    }
}
