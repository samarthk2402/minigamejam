using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    public float grappleSpeed = 10f;
    public LayerMask grappleableMask;
    public GameObject grappleHook;
    public float range;
    public float scrollSpeed = 5f;

    private LineRenderer lr;
    private Rigidbody2D rb;
    private DistanceJoint2D joint;

    private bool grappleShooting = false;
    private bool grappleRetracting = false;
    private bool grappleAttached = false;
    private bool grappleMissing = false;
    private bool sliding = false;
    private Vector2 grappleTarget;
    private Vector2 hookPos;
    private Vector3 slidingDir;
    private GameObject target;
    public bool targetAlive = false;

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
        lr.SetPosition(0, grappleHook.transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            if (grappleAttached)
            {
                grappleRetracting = true;
                grappleTarget = grappleHook.transform.position;
            }
            else
            {
                StartGrapple();
            }
        }

        if (grappleShooting)
        {
            ShootGrapple(grappleTarget);
            if (Vector2.Distance(hookPos, grappleTarget) <= 0.05f)
            {
                grappleShooting = false;
                if (target != null)
                {
                    grappleAttached = true;
                    target.GetComponent<Swing>().attached = true;
                }
            }
        }

        if (grappleRetracting)
        {
            ShootGrapple(grappleTarget);
            if (target != null)
            {
                target.GetComponent<Swing>().attached = false;
                target = null;
                grappleAttached = false;
                joint.enabled = false;
            }

            if (Vector2.Distance(hookPos, grappleTarget) <= 0.05f)
            {
                grappleRetracting = false;
                lr.positionCount = 1;
            }
        }

        if (grappleMissing)
        {
            ShootGrapple(grappleTarget);
            if (Vector2.Distance(hookPos, transform.position) >= range - 0.5f)
            {
                grappleMissing = false;
                grappleRetracting = true;
                grappleTarget = grappleHook.transform.position;
            }
        }

        if (grappleAttached)
        {
            if (targetAlive)
            {
                HandleGrappleAttached();
            }
            else
            {
                grappleAttached = false;
                grappleRetracting = true;
                grappleTarget = grappleHook.transform.position;
            }
        }

        if (sliding)
        {
            Slide(slidingDir);
        }
    }

    void HandleGrappleAttached()
    {
        joint.enabled = true;
        joint.connectedAnchor = target.transform.position;
        joint.distance = Vector2.Distance(grappleHook.transform.position, target.transform.position);
        lr.positionCount = 2;
        lr.SetPosition(1, hookPos);
        hookPos = Vector2.Lerp(hookPos, target.transform.position, grappleSpeed);

        // float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // if (scrollInput != 0f)
        // {
        //     slidingDir = ((target.transform.position - transform.position).normalized) * scrollInput;
        //     sliding = true;
        // }

        // if (Input.GetMouseButtonDown(2))
        // {
        //     sliding = false;
        //     rb.velocity = Vector2.zero;
        // }
    }

    void StartGrapple()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, mousePos - rb.position, range, grappleableMask);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            lr.positionCount = 2;

            grappleShooting = true;
            grappleTarget = hit.point;
            hookPos = grappleHook.transform.position;
            targetAlive = true;
        }
        else
        {
            lr.positionCount = 2;

            Vector2 homePos = new Vector2(grappleHook.transform.position.x, grappleHook.transform.position.y);

            grappleMissing = true;
            Vector2 dir = (mousePos - homePos).normalized;
            Vector2 extendedDir = homePos +   dir * range;
            grappleTarget = new Vector2(extendedDir.x, extendedDir.y);
            hookPos = homePos;
        }
    }

    void ShootGrapple(Vector2 target)
    {
        lr.positionCount = 2;
        lr.SetPosition(1, hookPos);
        hookPos = Vector2.Lerp(hookPos, target, grappleSpeed);
    }

    void Slide(Vector3 dir)
    {
        rb.velocity = dir * scrollSpeed;
    }
}
