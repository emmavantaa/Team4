using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLoop : MonoBehaviour {
    public GrapplingGun GrapplingScript;
    public KeyCode key;
    public Reticle GrappleCheck;
    public AudioSource BeamLoopAudioSource;

    // Start is called before the first frame update
    void Start() {
        BeamLoopAudioSource.Stop();

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(key) && GrappleCheck.canGrapple) {
            BeamLoopAudioSource.Play();
        }

        if (Input.GetKeyUp(key)) {
            BeamLoopAudioSource.Stop();
        }

    }
}
