using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ship>() != null)
        {
            FindObjectOfType<TutorialManager>().OnCarsDodged();
        }
    }
}
