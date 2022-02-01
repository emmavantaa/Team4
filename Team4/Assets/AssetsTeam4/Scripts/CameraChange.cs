using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera FirstPersonCam;
    //   public Camera ThirdPersonCam;

    void Awake()
    {
        FirstPersonCam.enabled = true;
        //     ThirdPersonCam.enabled = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            FirstPersonCam.enabled = !FirstPersonCam.enabled;
            //       ThirdPersonCam.enabled = !ThirdPersonCam.enabled;
        }


    }
}
