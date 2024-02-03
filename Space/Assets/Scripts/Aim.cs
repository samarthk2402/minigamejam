using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public LineRenderer lr;
    public Transform gun; 
    public Transform hook;
    public GameObject attached;
    public float grappleSpeed = 3f;
    public enum GrapplingState{
        Shooting,
        Retracting,
        Idle
    }

    public GrapplingState gState;

    private Vector3 hookEnd;
    private Vector3 targetPos;

    void Start(){
        lr = GetComponentInChildren<LineRenderer>();
        gState = GrapplingState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        switch(gState){
            case GrapplingState.Shooting:
                Shoot(targetPos);
                break;
            case GrapplingState.Retracting:
                Retract();
                break;
            case GrapplingState.Idle:

                if(attached != null){
                    lr.positionCount = 2;
                    lr.SetPosition(0, gun.position);
                    lr.SetPosition(1, attached.transform.position);
                    hookEnd = attached.transform.position;
                    if(Input.GetMouseButtonDown(0)){
                        attached.GetComponent<Swing>().attached = false;
                        attached = null;
                        gState = GrapplingState.Retracting;
                    }
                }else{
                    lr.positionCount = 0;
                    hookEnd = gun.position;
                    if(Input.GetMouseButtonDown(0)){
                        targetPos = mousePos;
                        gState = GrapplingState.Shooting;
                    }
                }
                break;
        }

        hook.position = hookEnd;
    }

    void Shoot(Vector3 target)
    {
        lr.positionCount = 2;
        lr.SetPosition(0, gun.position);

        // Use Vector3.Lerp to calculate the position of the hookEnd
        hookEnd = Vector3.Lerp(hookEnd, target, Time.deltaTime * grappleSpeed);
        lr.SetPosition(1, hookEnd);

        if(Vector3.Distance(hookEnd, target) <= 1f){
            hookEnd = target;
            lr.SetPosition(1, hookEnd);
            if(attached==null){
                gState = GrapplingState.Retracting;
            }else{
                gState = GrapplingState.Idle;
            }
        }

    }

    void Retract(){
        lr.positionCount = 2;
        lr.SetPosition(0, gun.position);
        hookEnd = Vector3.Lerp(hookEnd, gun.position, Time.deltaTime * grappleSpeed);
        lr.SetPosition(1, hookEnd);

        if(Vector3.Distance(hookEnd, gun.position) <= 1f){
            gState = GrapplingState.Idle;
        }
    }

}
