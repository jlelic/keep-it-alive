using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractiveItem : MonoBehaviour
{
    public ItemType itemType;

    Rigidbody2D rb2d;
    GameObject anchorPrefab;
    HingeJoint2D anchorHj2d;

    bool isBeingHeld = false;
    InteractionTarget hoveredTarget;

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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        bool hoveredTargetFound = false;
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<InteractionTarget>() != null)
            {
                if (hoveredTarget != null && hoveredTarget != hit.collider.gameObject.GetComponent<InteractionTarget>())
                {
                    hoveredTarget.itemHoverExit(itemType);
                }
                hoveredTarget = hit.collider.gameObject.GetComponent<InteractionTarget>();
                hoveredTarget.itemHoverEntered(itemType);
                hoveredTargetFound = true;
                break;
            }
        }
        if (!hoveredTargetFound && hoveredTarget != null) {
            hoveredTarget.itemHoverExit(itemType);
            hoveredTarget = null;
        }
    }

    void OnMouseUp()
    {
        isBeingHeld = false;
        if (hoveredTarget != null && hoveredTarget.acceptsType == itemType)
        {
            GameManager.Instance.ApplyItem(itemType);
            hoveredTarget.DoTheStuff();
            hoveredTarget.itemHoverExit(itemType);
            Destroy(gameObject);
        } else
        {
            if (hoveredTarget != null)
            {
                hoveredTarget.itemHoverExit(itemType);
            }
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - anchorHj2d.transform.position;
            Destroy(anchorHj2d.gameObject);
            rb2d.AddForce(100 * direction, ForceMode2D.Impulse);
        }
    }

    public bool IsBeingHeld()
    {
        return isBeingHeld;
    }
}
