using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public Transform head;
    public Transform followPoint;
    public Camera FirstPersonCam;
    public Camera ThirdPersonCam;



    void Update()
    {
        transform.position = player.transform.position;

        FirstPersonCam.transform.position = head.transform.position;
        //    ThirdPersonCam.transform.position = followPoint.transform.position;
    }
}
