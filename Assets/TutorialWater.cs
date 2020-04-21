using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWater: Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        FindObjectOfType<TutorialManager>().OnWaterStolen();
    }
}
