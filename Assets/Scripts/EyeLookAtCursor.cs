using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLookAtCursor : MonoBehaviour
{
    float maxAngle = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos -= new Vector3(0, 0, mousePos.z);
        Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
        var eulerZ = Mathf.Min(180+maxAngle, Mathf.Max(180-maxAngle, (transform.rotation.eulerAngles.z + 90) % 360));
        transform.eulerAngles = new Vector3(0, 0, eulerZ-90);
    }
}
