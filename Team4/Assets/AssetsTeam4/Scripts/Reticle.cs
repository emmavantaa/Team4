using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

    public float restingSize = 15;
    public float maxSize = 50;
    public float maxDistance = 50;
    public float speed = 15;
    public Transform aimingCamera;
    public LayerMask grappMask;

    private float currentSize;
    private bool canGrapple = false;

    //TODO color

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        GrappleCheck();
        if (canGrapple)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);

        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);

        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }

    private void GrappleCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin: aimingCamera.position, direction: aimingCamera.forward, out hit, maxDistance, grappMask))
        {
            Debug.Log("CAN");
            canGrapple = true;
        }
        else
        {
            canGrapple = false;
            Debug.Log("CANT");
        }

    }


}
