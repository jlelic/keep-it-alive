using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    public string id;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        InteractiveItem itemComponent = other.gameObject.GetComponent<InteractiveItem>();
        if (itemComponent != null && itemComponent.id == id) {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        sr.color = Color.white;
    }
}
