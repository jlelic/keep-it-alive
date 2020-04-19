using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    public ItemType acceptsType;

    private Color colorMatch = new Color(0, 1, 0, 0.5f);
    private Color colorNoMatch = new Color(1,0,0,0.5f);
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
                if (item != null && item.itemType == acceptsType) {
                    sr.color = colorMatch;
                }
                else
                {
                    sr.color = colorNoMatch;
                }
            }
        }
        if (!foundHeldItem)
        {
            sr.color = Color.clear;
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
        Debug.Log("target " + acceptsType + " did its stuff");
    }
}
