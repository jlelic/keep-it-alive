using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool startTimed = false;
    public bool shaking = false;
    public float shakeStrength = 0.1f;
    public float shakeInterval = 0.1f;
    
    float shakeDuration;
    Vector3 originalPosition;
    float lastDestinationChange = 0.0f;
    Vector3 destination;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (startTimed) {
            startTimed = false;
            TimedShake(1.0f);
        }
        if (shaking && Time.time - lastDestinationChange > shakeInterval)
        {
            lastDestinationChange = Time.time;
            Vector2 offset = Random.insideUnitCircle * shakeStrength;
            destination = originalPosition + new Vector3(offset.x, offset.y, originalPosition.z);
        }
    }

    void FixedUpdate()
    {
        if (shaking && destination.magnitude != 0)
        {
            Vector3 direction = destination - transform.position;
            transform.position += (0.1f / shakeInterval) * direction;
        }
    }

    public void TimedShake(float seconds)
    {
        shakeDuration = seconds;
        StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow()
    {
        originalPosition = transform.position;
        shaking = true;
        yield return new WaitForSeconds(shakeDuration);
        shaking = false;
        transform.position = originalPosition;
    }
}
