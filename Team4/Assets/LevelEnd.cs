using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    public bool isLast = false;
    protected Level_Loader levelLoader;
    void Start()
    {
        levelLoader = FindObjectOfType<Level_Loader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isLast)
        {
            levelLoader.LoadNextLevel();

        }
        else
        {

        }
    }
}
