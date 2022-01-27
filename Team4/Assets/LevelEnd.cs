using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    protected Level_Loader levelLoader;
    void Start()
    {
        levelLoader = FindObjectOfType<Level_Loader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        levelLoader.LoadNextLevel();
    }
}
