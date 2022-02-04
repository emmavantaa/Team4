using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public GameObject[] waypoints;
    public int num = 0;
    public float speed = 5;
    float wpradius = 1;

    //public Rigidbody rb;
    public GameObject player;
    public GameObject playerBody;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector3.Distance(waypoints[num].transform.position, transform.position) < wpradius) {

            num++;

            if (num >= waypoints.Length) {

                num = 0;

            }

        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[num].transform.position, Time.deltaTime * speed);

        // Moves the rb instead to be more "physics-based"
        //rb.MovePosition(Vector3.MoveTowards(transform.position, waypoints[num].transform.position, Time.deltaTime * speed));
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerBody)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerBody)
        {
            player.transform.parent = null;
        }
    }

}
