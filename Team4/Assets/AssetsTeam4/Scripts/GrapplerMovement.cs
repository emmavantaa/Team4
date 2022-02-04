using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplerMovement : MonoBehaviour
{
    public GameObject PlayerCamera;

    private float xRotation;
    private float yRotation;
    private float zRotation;

    void Awake()
    {
        yRotation = transform.localEulerAngles.y;
        zRotation = transform.localEulerAngles.z;
    }


    void Update()
    {
        xRotation = PlayerCamera.transform.eulerAngles.x;// + 90;

    }

    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(xRotation, yRotation, zRotation);
    }
}
