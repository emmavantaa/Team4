using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private SpringJoint joint;
    public LayerMask grappMask;

    /** Public variables */

    // Set what grapple can attach to
    public LayerMask whatIsGrappleable;

    // Where the rope starts from, where the raycast (aim) starts from, and where the player is located
    public Transform gunTip, aimingCamera, player;

    // How far can grapple be shot
    public float maxDistance = 100;


    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    // Called after every Update() call
    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin: aimingCamera.position, direction: aimingCamera.forward, out hit, maxDistance, grappMask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(a: player.position, b: grapplePoint);


            // The distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Gameplay variables - test and change if needed
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void DrawRope()
    {
        // If not grappling, don't draw the rope
        if (!joint) return;

        // Draws a rope from position 0 to position 1
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

}
