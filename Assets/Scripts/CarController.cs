using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject Hand;

    float TurningSpeed = 0.01f;

    bool canUseHand = true;

    Hand hand;

    void Start()
    {
        hand = GetComponentInChildren<Hand>();
    }


    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"),0,0)*TurningSpeed;
        if(Input.GetMouseButtonDown(0))
        {
            if(!canUseHand)
            {
                return;
            }


            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos -= new Vector3(0, 0, mousePos.z);
            Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
            Hand.transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

            var dir = mousePos - Hand.transform.position;
            var targetWorldPos = Hand.transform.position + dir * 0.3f;
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
