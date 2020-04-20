using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        iTween.ScaleBy(gameObject, iTween.Hash(
            "amount", new Vector3(1.1f, 1.1f, 1.1f),
            "time", 0.5f,
            "looptype", iTween.LoopType.pingPong,
            "easetype", iTween.EaseType.linear
            ));
        //var startRotation = 10f;
        //transform.eulerAngles = new Vector3(startRotation, startRotation, startRotation);
        //iTween.RotateTo(gameObject, iTween.Hash(
        //    "z", -startRotation,
        //    "time", 1f,
        //    "looptype", iTween.LoopType.pingPong,
        //    "easetype", iTween.EaseType.easeInOutBounce
        //     ));
    }
}
