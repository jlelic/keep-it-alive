using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiperManager : MonoBehaviour
{
    [SerializeField] Image mask;
    [SerializeField] Image dirtOverlay;
    [SerializeField] Sprite[] maskSprites;
    [SerializeField] Sprite[] dirtSprites;

    RectTransform maskTransform;

    public bool IsWiping { get; private set; }

    int dirtLevel = 0;

    void Start()
    {
        dirtOverlay.sprite = dirtSprites[dirtSprites.Length - 1];
        mask.sprite = maskSprites[0];
        maskTransform = mask.GetComponent<RectTransform>();
    }

    IEnumerator GetDirty()
    {
        yield return new WaitForSeconds(3);
        dirtOverlay.sprite = dirtSprites[1];
    }

    public void Dirty()
    {
        if(dirtLevel < dirtSprites.Length - 1)
        {
            dirtLevel++;
        }
        dirtOverlay.sprite = dirtSprites[dirtLevel];
    }

    public void Wipe()
    {
        if(IsWiping)
        {
            return;
        }
        IsWiping = true;
        StartCoroutine(Wiping());
    }

    IEnumerator Wiping()
    {
        for (int i = 0; i < maskSprites.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            mask.sprite = maskSprites[i];
        }
        yield return new WaitForSeconds(0.1f);
        dirtOverlay.sprite = dirtSprites[0];
        mask.sprite = maskSprites[0];
        IsWiping = false;
    }
}
