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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var hand = other.GetComponent<Hand>();
        if(hand != null && !hasBeenGrabbed)
        {
            var parentPerson = GetComponentInParent<SaddablePerson>();
            if (parentPerson != null)
            {
                parentPerson.MakeSad();
            }
            hand.GrabItem(this);
            hasBeenGrabbed = true;
        }
    }

    public void SpawnInventoryPrefab(int index)
    {
        if (itemManager != null && spawnPrefab != null) {
            itemManager.SpawnItem(spawnPrefab, index);
        }
    }
}
