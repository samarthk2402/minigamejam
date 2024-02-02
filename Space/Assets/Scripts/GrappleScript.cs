using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    public float grappleSpeed = 10f;
    public LayerMask grappleableMask;

    public GameObject grappleHook;
    private LineRenderer lr;

    //private bool isGrappling = false;
    private Rigidbody2D rb;
    private DistanceJoint2D joint;

    private bool grappleShooting = false;
    private bool grappleRetracting = false;
    private bool grappleAttached = false;
    private bool grappleMissing = false;
    private Vector2 grappleTarget;
    public GameObject target; 
    private Vector2 hookPos;

    public float range;

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
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Grapple!");
            if(grappleAttached){
                grappleRetracting = true;
                grappleTarget = grappleHook.transform.position;
                //hookPos = grappleHook.transform.position;
            }else{
                StartGrapple();
            }
        }

        if(grappleShooting){

            ShootGrapple(grappleTarget);

            if(Vector2.Distance(hookPos, grappleTarget) <= 0.05f){
                grappleShooting = false;
                grappleAttached = true;
                target.GetComponent<Swing>().attached = true;
                //Debug.Log("reached");
            }
        }

        if(grappleRetracting){
            ShootGrapple(grappleTarget);

            if(Vector2.Distance(hookPos, grappleTarget) <= 0.05f){
                grappleRetracting = false;
                grappleAttached = false;
                target.GetComponent<Swing>().attached = false;
                target = null;
                //Debug.Log("reached");
            }
        }

        if(grappleMissing){
            ShootGrapple(grappleTarget);

            if(Vector2.Distance(hookPos, transform.position) >= range-0.5f){
                grappleMissing = false;
                grappleRetracting = true;
                grappleTarget = grappleHook.transform.position;
                //Debug.Log("Return");
            }
        }

        if(grappleAttached){
            lr.positionCount = 2;
            lr.SetPosition(1, hookPos);
            hookPos = Vector2.Lerp(hookPos, target.transform.position, grappleSpeed);
        }
    }

    void StartGrapple()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, mousePos - rb.position, range, grappleableMask);
        //Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            //isGrappling = true;
            joint.enabled = true;
            joint.connectedAnchor = hit.point;
            joint.distance = Vector2.Distance(rb.position, hit.point);

            lr.positionCount = 2;

            grappleShooting = true;
            grappleTarget = hit.point;
            hookPos = grappleHook.transform.position;

        }else{
            lr.positionCount = 2;

            Vector2 homePos = new Vector2(grappleHook.transform.position.x, grappleHook.transform.position.y);

            grappleMissing = true;
            Vector2 dir = mousePos - homePos;
            dir.Normalize();
            Vector2 extendedDir = homePos + dir*range;
            grappleTarget = new Vector2(extendedDir.x, extendedDir.y);
            hookPos = homePos;
        }
    }

    private void ShootGrapple(Vector2 target){
            lr.positionCount = 2;
            lr.SetPosition(1, hookPos);
            hookPos = Vector2.Lerp(hookPos, target, grappleSpeed);
    }

    // void StopGrapple()
    // {
    //     isGrappling = false;
    //     joint.enabled = false;
    // }
}

