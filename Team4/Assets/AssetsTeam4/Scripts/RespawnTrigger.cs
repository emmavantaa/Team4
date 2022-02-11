using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    //[SerializeField] private LayerMask layerToReact;
    [SerializeField] private string tagToReact = "Player";
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private GameObject visualization;
    bool m_Started;
    protected Level_Loader levelLoader;


    void Awake()
    {
        visualization.GetComponent<MeshRenderer>().enabled = false;
        levelLoader = FindObjectOfType<Level_Loader>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == tagToReact)
        {
            levelLoader.ReloadCurrentLevel();


            /** For use with respawn point instead of level reload: */

           //  StartCoroutine(ResetLocation(other, 1));
           //  other.transform.position = spawnLocation.position;
        }
    }


    /** For use with respawn point instead of level reload: */

    /*
    IEnumerator ResetLocation(Collider other, float delay)
    {
        yield return new WaitForSeconds(delay);
        other.transform.position = spawnLocation.position;
    }
    */
    
}
