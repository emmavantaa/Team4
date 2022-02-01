using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    [SerializeField] private GrapplingGun grapplingScript;
    public Rigidbody rb;
    private float speed = 12f;
    private float gravity = -10f;
    private float jumpHeight = 4f;
    private float groundDistance = 0.2f;
    private Vector3 velocity;
    private float swingVelocity;
    private bool isGrounded;
    private bool justGRappled;

    private bool isSwinging = false;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!isGrounded && grapplingScript.Grappling())
        {
            controller.enabled = false;
            if (justGRappled)
            {
                // rb.AddForce(velocity);
                justGRappled = false;
            }
            isSwinging = true;
        }
        else
        {
            if (isSwinging)
            {
                velocity.y = Mathf.Sqrt(gravity * -2 * gravity);
                if (isGrounded)
                {
                    isSwinging = false;
                }
            }

            else
            {
                rb.velocity = Vector3.zero;
                controller.enabled = true;
                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);

                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = transform.right * x + transform.forward * z;

                controller.Move(move * speed * Time.deltaTime);
            }

        }




        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }



    }

}

