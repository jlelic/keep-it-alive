using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractiveItem : MonoBehaviour
{
    public string id;

    Rigidbody2D rb2d;
    GameObject anchorPrefab;
    HingeJoint2D anchorHj2d;

    bool isBeingHeld = false;
    InteractionTarget collidingTarget;

    void Start()
    {
        anchorPrefab = Resources.Load("DragAnchor") as GameObject;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        isBeingHeld = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject anchorInstance = Instantiate(anchorPrefab, mouseWorldPos, transform.rotation) as GameObject;
        anchorHj2d = anchorInstance.GetComponent<HingeJoint2D>();
        anchorHj2d.connectedBody = rb2d;
        anchorHj2d.connectedAnchor = transform.InverseTransformPoint(mouseWorldPos); 
    }

    void OnMouseDrag()
    {
        anchorHj2d.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        isBeingHeld = false;
        if (collidingTarget != null) {
            collidingTarget.DoTheStuff();
            Destroy(gameObject);
        }
        Destroy(anchorHj2d.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        InteractionTarget targetComponent = other.gameObject.GetComponent<InteractionTarget>();
        if (targetComponent != null && targetComponent.id == id) {
            collidingTarget = targetComponent;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        InteractionTarget targetComponent = other.gameObject.GetComponent<InteractionTarget>();
        if (targetComponent != null && targetComponent.id == id) {
            collidingTarget = null;
        }
    }

    public bool IsBeingHeld()
    {
        return isBeingHeld;
    }
}
