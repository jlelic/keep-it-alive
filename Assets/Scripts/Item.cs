using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

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
        // TODO        
    }
}
