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

    bool wiping = false;

    void Start()
    {
        dirtOverlay.sprite = dirtSprites[0];
        mask.sprite = maskSprites[0];
    }

    IEnumerator GetDirty()
    {
        yield return new WaitForSeconds(3);
        dirtOverlay.sprite = dirtSprites[1];
    }

    public void Wipe()
    {
        if(wiping)
        {
            return;
        }
        wiping = true;
        StartCoroutine(Wiping());
    }


    IEnumerator Wiping()
    {
        for (int i = 0; i < maskSprites.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            mask.sprite = maskSprites[i];
        }
        yield return new WaitForSeconds(5f);
        dirtOverlay.sprite = dirtSprites[1];
        mask.sprite = maskSprites[0];
        wiping = false;
    }
}
