using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
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
        }
    }

}
