using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    public ItemType acceptsType;

    private Color colorMatch = new Color(0, 1, 0, 0.5f);
    private Color colorNoMatch = new Color(1,0,0,0.5f);
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void itemHoverEntered(ItemType type)
    {
        if (type == acceptsType)
        {
            sr.color = colorMatch;
        } else 
        {
            sr.color = colorNoMatch;
        }
    }
    public void itemHoverExit(ItemType type)
    {
        sr.color = Color.clear;
    }

    public void DoTheStuff()
    {
        //TODO
        Debug.Log("target " + acceptsType + " did its stuff");
    }
}
