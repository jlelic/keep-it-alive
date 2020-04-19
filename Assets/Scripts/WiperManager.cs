using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiperManager : MonoBehaviour
{
    [SerializeField] Image mask;
    [SerializeField] Image water;
    [SerializeField] Image dirtOverlay;
    [SerializeField] Sprite[] maskSprites;
    [SerializeField] Sprite[] dirtSprites;
    [SerializeField] RectTransform uiWiper;
    [SerializeField] GameObject shipWiper;


    RectTransform maskTransform;

    public bool IsWiping { get; private set; }

    int dirtLevel = 0;

    void Start()
    {
        dirtOverlay.sprite = dirtSprites[dirtSprites.Length-1];
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

    public void Wipe(bool withWater)
    {
        if(IsWiping)
        {
            return;
        }
        IsWiping = true;

        var wSize = uiWiper.sizeDelta;
        uiWiper.gameObject.SetActive(true);
        uiWiper.sizeDelta = new Vector2(2f * Screen.height * wSize.x / wSize.y, 2 * Screen.height);
        uiWiper.anchoredPosition = new Vector2(0, 0);
        uiWiper.rotation = Quaternion.Euler(new Vector3(0, 0, 130));

        if (withWater)
        {
            water.gameObject.SetActive(true);
            water.color = Color.clear;
            Utils.tweenColor(water, Color.white, 0.2f);
        }
             
        iTween.RotateAdd(uiWiper.gameObject, iTween.Hash(
            "z", -220,
            "time", 0.7f,
            "delay", 0.3f,
            "easetype", iTween.EaseType.linear,
//            "delay", 0.14f,
            "oncomplete", (Action)(() => {
                iTween.RotateAdd(uiWiper.gameObject,
                    iTween.Hash(
                        "z", 220,
                        "time", 1,
                        "easetype", iTween.EaseType.linear,
                        "oncomplete", (Action)(() => {
                            uiWiper.gameObject.SetActive(false);
                            IsWiping = false;
                        })
                    )); ;
            })
        ));

        iTween.RotateAdd(shipWiper.gameObject, iTween.Hash(
            "z", 150,
            "time", 0.7f,
            "delay", 0.5f,
            "easetype", iTween.EaseType.linear,
            //            "delay", 0.14f,
            "oncomplete", (Action)(() => {
                iTween.RotateAdd(shipWiper.gameObject,
                    iTween.Hash(
                        "z", -150,
                        "time", 1,
                        "easetype", iTween.EaseType.linear
                    )); ;
            })
        ));

        if(withWater)
        {
            StartCoroutine(Wiping());
        }
    }

    IEnumerator Wiping()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < maskSprites.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            mask.sprite = maskSprites[i];
        }
        yield return new WaitForSeconds(0.1f);
        dirtOverlay.sprite = dirtSprites[0];
        mask.sprite = maskSprites[0];
        water.gameObject.SetActive(false);
    }
}
