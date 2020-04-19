using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ParticleSystem impactParticleEffect;
    public Shake cameraShake;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody == null)
        {
            return;
        }

        if (collision.rigidbody.gameObject.GetComponent<EnemyCar>() != null)
        {
            GameManager.Instance.TakeHit();
            GameManager.Instance.DirtyWindow();

            if (impactParticleEffect != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                Vector3 contactPosition = new Vector3(contactPoint.x, contactPoint.y, -0.5f);
                impactParticleEffect.gameObject.transform.position = contactPosition;
                impactParticleEffect.Play();
                cameraShake.TimedShake(0.5f);
            }
        }
    }

}
