using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public GameObject[] waypoints;
    public int num = 0;
    public float speed = 5;
    float wpradius = 1;
    public GameObject MachineObject;
    private Battery MachineBattery;

    public Rigidbody rb;
    public GameObject player;
    public bool isPoweredByMachine;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MachineBattery = MachineObject.GetComponent<Battery>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MachineBattery.GetBatteryFull() || !isPoweredByMachine)
        {
            if (Vector3.Distance(waypoints[num].transform.position, transform.position) < wpradius)
            {
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

}
