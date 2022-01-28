using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public GameObject PlayerCamera;

    void Awake()
    {
        
    }


    void Update()
    {
        transform.rotation = PlayerCamera.transform.rotation;
    }
}
