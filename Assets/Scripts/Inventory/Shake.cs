using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeStrength = 1.0f;
    public float shakeInterval = 1.0f;
    
    Vector3 originalPosition;
    float lastDestinationChange = 0.0f;
    Vector3 destination;

    void Start() {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Time.time - lastDestinationChange > shakeInterval)
        {
            lastDestinationChange = Time.time;
            Vector3 offset = Random.insideUnitSphere * shakeStrength;
            offset.z = transform.position.z;
            Debug.Log(offset);
            destination = originalPosition + offset;
            Debug.Log(destination);
        }
    }

    void FixedUpdate()
    {
        if (destination != null && lastDestinationChange > 0)
        {
            Vector3 direction = destination - transform.position;
            transform.position += (0.1f / shakeInterval) * direction;
        }
    }
}
