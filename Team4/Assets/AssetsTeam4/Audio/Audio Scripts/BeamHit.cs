using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeamHit : MonoBehaviour {

    public GrapplingGun GrapplingScript;
    public Reticle GrappleCheck;
    public KeyCode key;
    public AudioSource source;
    public AudioClip Hit;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(key) && GrappleCheck.canGrapple) {
            source.PlayOneShot(Hit);
        }

    }
}
