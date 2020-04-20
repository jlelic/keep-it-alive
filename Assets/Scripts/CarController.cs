using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] SpriteRenderer leftFire;
    [SerializeField] SpriteRenderer rightFire;
    [SerializeField] Animator tentacleAnimator;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!canUseHand)
            {
                return;
            }

            canUseHand = false;

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos -= new Vector3(0, 0, mousePos.z);
            Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
            var rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
            hand.transform.rotation = rotation;
            tentacleAnimator.transform.rotation = rotation;
            tentacleAnimator.Play("tentacle");

            var dir = (mousePos - hand.transform.position).normalized;
            var targetWorldPos = hand.transform.position + dir * 5.3f;
            var targetPos = targetWorldPos - transform.position;
            var originalPos = hand.transform.localPosition;
            // callback hell (:
            iTween.MoveTo(hand.gameObject, 
                iTween.Hash(
                    "position", targetPos,
                    "time", 0.6f,
                    "isLocal", true,
                    "oncomplete", (Action)(() => {
                        iTween.MoveTo(hand.gameObject,
                            iTween.Hash(
                                "position", originalPos,
                                "time", 0.4f,
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

    bool ClickedInventoryItem() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.layer == 8)
            {
                return true;
            }
        }
        return false;
    }
}
