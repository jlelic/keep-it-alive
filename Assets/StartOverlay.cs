using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOverlay : MonoBehaviour
{
    float time = 1;
    void Start()
    {
        var img = GetComponent<UnityEngine.UI.Image>();
        img.color = Color.black;
        Utils.tweenColor(img, Color.clear, time);
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
