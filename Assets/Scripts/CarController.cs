using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public GameObject Hand;
    [SerializeField] SpriteRenderer leftFire;
    [SerializeField] SpriteRenderer rightFire;
    float TurningSpeed = 30;
    bool canUseHand = true;
    Hand hand;
    Rigidbody2D rigidbody;
    GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;
        hand = GetComponentInChildren<Hand>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //transform.position += new Vector3(Input.GetAxis("Horizontal"),0,0)*Mathf.Min(TurningSpeed, GameManager.Instance.CarSpeed);
        float axis = Input.GetAxis("Horizontal");
        if (GM.MaxSpeed > 0)
        {
            rigidbody.AddForce(new Vector2(axis, 0) * (TurningSpeed * GM.CarSpeed / GM.MaxSpeed));
            rightFire.color = new Color(1, 1, 1, axis >= 0 ? 1 - axis : 1);
            leftFire.color = new Color(1, 1, 1, axis <= 0 ? 1 + axis : 1);
        }
        else
        {
            rightFire.color = Color.clear;
            leftFire.color = Color.clear;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(!canUseHand)
            {
                return;
            }

            canUseHand = false;

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos -= new Vector3(0, 0, mousePos.z);
            Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
            Hand.transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

            var dir = (mousePos - Hand.transform.position).normalized;
            var targetWorldPos = Hand.transform.position + dir * 5.3f;
            var targetPos = targetWorldPos - transform.position;
            var originalPos = Hand.transform.localPosition;
            // callback hell (:
            iTween.MoveTo(Hand, 
                iTween.Hash(
                    "position", targetPos,
                    "time", 1,
                    "isLocal", true,
                    "oncomplete", (Action)(() => {
                        iTween.MoveTo(Hand,
                            iTween.Hash(
                                "position", originalPos,
                                "time", 0.6f,
                                "isLocal", true,
                                "oncomplete", (Action)(() => {
                                    canUseHand = true;
                                    hand.AddItemsToInventory();
                                })
                            )); ;
                    })
                ));
        }
    }
}
