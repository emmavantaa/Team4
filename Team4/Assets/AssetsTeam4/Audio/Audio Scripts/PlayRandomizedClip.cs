using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayRandomizedClip : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;

    public void Play() {
        if (clips.Length == 0) {
            Debug.LogError("Add audio clips in inspector!", this);
            return;
        }

        var clip = clips[Random.Range(0, clips.Length)];
        source.PlayOneShot(clip);
    }
}
