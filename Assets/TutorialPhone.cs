using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhone: Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        FindObjectOfType<TutorialManager>().OnPhoneStolen();
    }
}
