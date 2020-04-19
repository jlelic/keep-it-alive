using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject spawnPrefab;

    ItemManager itemManager;

    void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hand = other.GetComponent<Hand>();
        if(hand != null)
        {
            hand.GrabItem(this);
        }
    }

    public void SpawnInventoryPrefab(int index)
    {
        if (itemManager != null && spawnPrefab != null) {
            itemManager.SpawnItem(spawnPrefab, index);
        }
    }
}
