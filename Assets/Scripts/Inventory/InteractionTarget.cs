using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    public string id;

    SpriteRenderer sr;
    List<InteractiveItem> collidingItems = new List<InteractiveItem>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bool foundHeldItem = false;
        foreach(InteractiveItem item in collidingItems) {
            if (item != null && item.IsBeingHeld()) 
            {
                foundHeldItem = true;
                if (item != null && item.id == id) {
                    sr.color = Color.green;
                }
                else
                {
                    sr.color = Color.red;
                }
            }
        }
        if (!foundHeldItem)
        {
            sr.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        InteractiveItem itemComponent = other.gameObject.GetComponent<InteractiveItem>();
        collidingItems.Add(itemComponent);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        InteractiveItem itemComponent = other.gameObject.GetComponent<InteractiveItem>();
        collidingItems.Remove(itemComponent);
    }

    public void DoTheStuff()
    {
        //TODO
        Debug.Log("target " + id + " did its stuff");
    }
}
