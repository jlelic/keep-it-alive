using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdustPosition : MonoBehaviour
{
    public float rightOffset = 0;
    public float bottomOffset = 0;

    void Update()
    {
        float cameraBottom = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;
        float cameraRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        transform.position = new Vector3(cameraRight - rightOffset, cameraBottom + bottomOffset, 0);
    }
}
