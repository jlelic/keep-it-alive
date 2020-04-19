using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject spawnPrefab;

    ItemManager itemManager;
    bool hasBeenGrabbed = false;

    void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hand = other.GetComponent<Hand>();
        if(hand != null && !hasBeenGrabbed)
        {
            hand.GrabItem(this);
            hasBeenGrabbed = true;
        }
    }

    public void SpawnInventoryPrefab(int index)
    {
        if (itemManager != null && spawnPrefab != null) {
            Debug.Log("YEAH");
            itemManager.SpawnItem(spawnPrefab, index);
        }
    }
}
