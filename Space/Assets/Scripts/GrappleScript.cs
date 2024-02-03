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
    public float scrollSpeed = 5f;
    public bool sliding = false;
    public Vector3 sliding_dir;

    public bool target_alive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        lr = grappleHook.GetComponent<LineRenderer>();
        lr.positionCount = 1;
    }

    void Update()
    {
        //Debug.Log(target);
        lr.SetPosition(0, grappleHook.transform.position);
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
                if(target != null){
                    grappleAttached = true;
                    target.GetComponent<Swing>().attached = true;
                }
                //Debug.Log("reached");
            }
        }

        if(grappleRetracting){
            ShootGrapple(grappleTarget);
            if(target != null){
                    target.GetComponent<Swing>().attached = false;
                    target = null;
                    grappleAttached = false;
                    joint.enabled = false;
            }

            if(Vector2.Distance(hookPos, grappleTarget) <= 0.05f){
                grappleRetracting = false;
                lr.positionCount = 1;
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
            if(target_alive){

                joint.enabled = true;
                joint.connectedAnchor = target.transform.position;
                joint.distance = Vector2.Distance(grappleHook.transform.position, target.transform.position);
                lr.positionCount = 2;
                lr.SetPosition(1, hookPos);
                hookPos = Vector2.Lerp(hookPos, target.transform.position, grappleSpeed);

                float scrollInput = Input.GetAxis("Mouse ScrollWheel");

                // Check if there is any scroll input
                if (scrollInput != 0f)
                {
                    // Scroll detected, do something with the input
                    // For example, you can move an object up or down based on the scroll input
                    // Here, we'll just print the scroll input to the console
                    //Debug.Log("Scroll Input: " + scrollInput);

                    // You can also use the input to modify a variable or perform other actions
                    // For example, adjusting the camera's field of view based on the scroll input
                    sliding_dir = ((target.transform.position - transform.position).normalized)*scrollInput;
                    sliding = true;
                }

                if(Input.GetMouseButtonDown(2)){
                    sliding = false;
                    rb.velocity = Vector2.zero;
                }
            }else{
                grappleAttached = false;
                grappleRetracting = true;
                grappleTarget = grappleHook.transform.position;
            }
        }

        if(sliding){
            Slide(sliding_dir);
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

            lr.positionCount = 2;

            grappleShooting = true;
            grappleTarget = hit.point;
            hookPos = grappleHook.transform.position;
            target_alive = true;

        }else{
            lr.positionCount = 2;

            Vector2 homePos = new Vector2(grappleHook.transform.position.x, grappleHook.transform.position.y);

            grappleMissing = true;
            Vector2 dir = (mousePos - homePos).normalized;
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

    private void Slide(Vector3 dir){
        rb.velocity = dir*scrollSpeed;
        //Debug.Log(dir);
    }

    // void StopGrapple()
    // {
    //     isGrappling = false;
    //     joint.enabled = false;
    // }
}

