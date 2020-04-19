using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ParticleSystem impactParticleEffect;
    public Shake cameraShake;

    SpriteRenderer sr;
    bool invulnerable = false;
    float invulnerabilityDuration = 1.7f;
    Color invulnerabilityColor = Color.cyan;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        invulnerabilityColor.a = 0.75f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody == null)
        {
            return;
        }

        if (collision.rigidbody.gameObject.GetComponent<EnemyCar>() != null)
        {
            if (!invulnerable) {
                GameManager.Instance.TakeHit();
                GameManager.Instance.DirtyWindow();
                StartCoroutine("MakeInvulnerable");
            }
            

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

    IEnumerator MakeInvulnerable()
    {
        invulnerable = true;
        Utils.tweenColor(sr, invulnerabilityColor, 0.3f, 0, iTween.EaseType.easeInOutQuad, iTween.LoopType.pingPong);
        yield return new WaitForSeconds(invulnerabilityDuration);
        invulnerable = false;
        iTween.Stop(gameObject);
        sr.color = Color.white;
    }
}
