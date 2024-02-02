using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    public float grappleSpeed = 10f;
    public LayerMask grappleableMask;

    public GameObject grappleHook;
    private LineRenderer lr;

    private bool isGrappling = false;
    private Rigidbody2D rb;
    private DistanceJoint2D joint;

    private bool grappleShooting = false;
    private Vector2 grappleTarget;
    private Vector2 hookPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        lr = grappleHook.GetComponent<LineRenderer>();
        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            //Debug.Log("Grapple!");
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0) && isGrappling)
        {
            StopGrapple();
        }

        if(grappleShooting){
            lr.positionCount = 2;
            lr.SetPosition(1, hookPos);
            hookPos = Vector2.Lerp(hookPos, grappleTarget, grappleSpeed);

            if(Vector2.Distance(hookPos, grappleTarget) <= 0.05f){
                grappleShooting = false;
                Debug.Log("reached");
            }
        }
    }

    void StartGrapple()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, mousePos - rb.position, 100f, grappleableMask);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            isGrappling = true;
            joint.enabled = true;
            joint.connectedAnchor = hit.point;
            joint.distance = Vector2.Distance(rb.position, hit.point);

            lr.positionCount = 2;
            grappleShooting = true;
            grappleTarget = hit.point;
            hookPos = transform.position;
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        joint.enabled = false;
    }
}

