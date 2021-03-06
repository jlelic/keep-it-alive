﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceHit;

    bool isOk = true;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOk = false;
    }

    public void PlayHitSound()
    {
        Utils.PlayAudio(audioSourceHit, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOk)
        {
            var fw = transform.up;
            rigidbody.velocity = new Vector2(fw.x, fw.y) * 2;
        }
    }
}
