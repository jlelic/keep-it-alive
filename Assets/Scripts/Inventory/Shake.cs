using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool enabled = false;
    public float shakeStrength = 0.1f;
    public float shakeInterval = 0.1f;
    
    Vector3 originalPosition;
    float lastDestinationChange = 0.0f;
    Vector3 destination;

    void Start() {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (enabled && Time.time - lastDestinationChange > shakeInterval)
        {
            lastDestinationChange = Time.time;
            Vector2 offset = Random.insideUnitCircle * shakeStrength;
            destination = originalPosition + new Vector3(offset.x, offset.y, originalPosition.z);
        }
    }

    void FixedUpdate()
    {
        if (enabled && destination.magnitude != 0)
        {
            Vector3 direction = destination - transform.position;
            transform.position += (0.1f / shakeInterval) * direction;
        }
    }
}
