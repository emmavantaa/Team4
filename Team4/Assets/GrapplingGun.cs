using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] private GrapplingGun grapplingScript;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private Vector3 startPoint;
    private SpringJoint joint;
    private Battery battery;
    public Battery machineBattery;
    public Generator generator;
    private float maxDistance = 50;

    private bool isShooting, isGrounded;

    public bool isGrappling;

    public LayerMask whatIsGrappleable;
    public GameObject playerObject;
    public Transform beamStartPoint, aimingCamera, player;

    private enum grappleState { grapple, generator, machine, none };

    private grappleState currentGrappleState = grappleState.none;

    public float jointSpring = 4f;
    public float jointDamper = 7f;
    public float massScale = 4.5f;
    public float jointSpringGrounded = 4f;

    void Awake()
    {
        battery = playerObject.GetComponent<Battery>();
        lr = GetComponent<LineRenderer>();
        isShooting = false;
        isGrappling = false;
    }


    void Update()
    {
        isGrounded = playerObject.GetComponent<PlayerMovement_GrapplingDemo>().isGrounded;

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
            LayerMask layerHit = hit.transform.gameObject.layer;

            if (layerHit.value == 11)
            {
                isGrappling = true;
                currentGrappleState = grappleState.grapple;
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

                if (isGrounded)
                {
                    joint.spring = jointSpringGrounded;
                }
                else
                {
                    joint.spring = jointSpring;
                }
                joint.damper = jointDamper;
                joint.massScale = massScale;



                lr.positionCount = 2;
            }

            else if (layerHit.value == 12) // Generator
            {
                currentGrappleState = grappleState.generator;
                // For rope
                isGrappling = true;
                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = true;
                joint.connectedAnchor = grapplePoint;
                lr.positionCount = 2;
                //
                generator = hit.transform.gameObject.GetComponent<Generator>();

                if (!generator.IsEmpty())
                {
                    Debug.Log("Generator not Empty");
                    generator.StartDrain(battery);
                }
                else
                {
                    Debug.Log("Generator Empty");
                    isGrappling = false;
                }
            }
            else if (layerHit.value == 13)// Machine
            {
                currentGrappleState = grappleState.machine;
                machineBattery = hit.transform.gameObject.GetComponent<Battery>();
                // For rope
                isGrappling = true;
                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = true;
                joint.connectedAnchor = grapplePoint;
                lr.positionCount = 2;
                //
                if (battery.GetBatteryChargeLeft(1) && !machineBattery.GetBatteryFull())
                {
                    machineBattery.StartCharge();
                    battery.StartDrain();
                }
                else if (machineBattery.GetBatteryFull())
                {
                    Debug.Log("Machine Battery Full");
                    isGrappling = false;

                    machineBattery.StopCharge();
                    battery.StopDrain();
                }
                else
                {
                    Debug.Log("Player Battery Empty");
                    isGrappling = false;

                    machineBattery.StopCharge();
                    battery.StopDrain();
                }
            }
            else
            {

                isGrappling = false;
            }
        }
    }
    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void DrawRope()
    {
        // If not grappling, don't draw the rope
        if (!isGrappling) return;

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

        //


        if (currentGrappleState != grappleState.none)

            switch (currentGrappleState)
            {
                case grappleState.grapple:
                    break;

                case grappleState.generator:
                    generator.StopDrain();
                    break;

                case grappleState.machine:
                    machineBattery.StopCharge();
                    battery.StopDrain();
                    break;
            }
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

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
