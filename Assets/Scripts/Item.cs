using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemManager itemManager;
    public GameObject spawnPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        var hand = other.GetComponent<Hand>();
        if(hand != null)
        {
            hand.GrabItem(this);
        }
    }

    public void SpawnInventoryPrefab()
    {
        if (itemManager != null && spawnPrefab != null) {
            itemManager.SpawnItem(spawnPrefab);
        }
    }
}
