using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Item> grabbedItems = new List<Item>();
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GrabItem(Item item)
    {
        Utils.PlayAudio(audioSource, true);
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
