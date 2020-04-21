using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ParticleSystem impactParticleEffect;
    public Shake cameraShake;

    SpriteRenderer sr;
    bool invulnerable = false;
    float invulnerabilityDuration = 3.5f;
    Color invulnerabilityColor = Color.cyan;
    int insideDirt = 0;
    ParticleSystemsManager particleManager;
    WiperManager wiperManager;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        invulnerabilityColor.a = 0.75f;
        particleManager = FindObjectOfType<ParticleSystemsManager>();
        wiperManager = FindObjectOfType<WiperManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody == null)
        {
            return;
        }

        Vector2 contactPoint = collision.contacts[0].point;
        Vector3 contactPosition = new Vector3(contactPoint.x, contactPoint.y, -0.5f);

        if (collision.rigidbody.GetComponent<EnemyCar>() != null)
        {
            collision.rigidbody.GetComponent<EnemyCar>().PlayHitSound();

            if (!invulnerable) {
                GameManager.Instance.TakeHit();
                GameManager.Instance.DirtyWindow();
                StartCoroutine("MakeInvulnerable");
            }
            

            if (impactParticleEffect != null)
            {
                impactParticleEffect.transform.position = contactPosition;
                impactParticleEffect.Play();
                cameraShake.TimedShake(0.5f);
            }
        }
        else if (collision.rigidbody.GetComponent<Dirt>() != null)
        {
            collision.collider.isTrigger = true;
            if (insideDirt == 0)
            {
                wiperManager.Dirty();
                particleManager.EnterDirt(contactPosition);
            }
            insideDirt++;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Dirt>() != null)
        {
            if(insideDirt > 0)
            {
                insideDirt--;
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
