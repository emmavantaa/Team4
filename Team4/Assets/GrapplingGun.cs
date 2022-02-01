using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private Vector3 startPoint;
    private SpringJoint joint;
    private float maxDistance = 50;

    private bool isShooting, isGrappling;

    public LayerMask whatIsGrappleable;
    public Transform beamStartPoint, aimingCamera, player;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        isShooting = false;
        isGrappling = false;
    }


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
        isShooting = true;
        startPoint = player.position;

        RaycastHit hit;
        if (Physics.Raycast(origin: aimingCamera.position, direction: aimingCamera.forward, out hit, maxDistance, layerMask: whatIsGrappleable))
        {
            isGrappling = true;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            //float distanceFromPoint = Vector3.Distance(a: player.position, b: grapplePoint);
            float distanceFromPoint = Vector3.Distance(a: startPoint, b: grapplePoint);

            // The distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Gameplay variables - test and change if needed
            joint.spring = 4f; // <-- original was 4, tried out what much bigger value would do. -Topi
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
        else
        {
            isGrappling = false;
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
        lr.SetPosition(0, beamStartPoint.position);
        lr.SetPosition(1, grapplePoint);
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        isShooting = false;
        isGrappling = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    /// <summary>
    /// Function to be called from FPMovement
    /// </summary>
    /// <returns> private bool isGrappling </returns>
    public bool Grappling()
    {
        return isGrappling;
    }

    /// <summary>
    /// For pulling the player towards the grapple point.
    /// Not yet implemented, probably won't work as it is.
    /// </summary>
    private void GrapplePull()
    {/*
        Vector3 grappleDir = (grapplePoint - player.position).normalized;
        player.position = player.position + (grappleDir * 0.2f);
        */
    }
}
