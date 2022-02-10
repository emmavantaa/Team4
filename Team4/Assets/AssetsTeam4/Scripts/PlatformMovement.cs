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

    public GameObject MachineObject;
    private Battery MachineBattery;

    public Rigidbody rb;
    public bool isPoweredByMachine;

    private bool isWaiting = false;



    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        MachineBattery = MachineObject.GetComponent<Battery>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((MachineBattery.GetBatteryFull() && !isWaiting) || (!isPoweredByMachine && !isWaiting))
        {
            if (Vector3.Distance(waypoints[num].transform.position, transform.position) < wpradius)
            {
                StartCoroutine(Wait());
                num++;
                if (num >= waypoints.Length)
                {
                    num = 0;
                }
            }
            //transform.position = Vector3.MoveTowards(transform.position, waypoints[num].transform.position, Time.deltaTime * speed);
            rb.MovePosition(Vector3.MoveTowards(transform.position, waypoints[num].transform.position, Time.deltaTime * speed));
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }


}