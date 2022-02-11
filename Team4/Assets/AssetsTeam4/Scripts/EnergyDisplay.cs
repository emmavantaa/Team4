using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{

    [SerializeField] private float energy; // This should eventually come from the player / manager holding the energy amount
    [SerializeField] GrapplingGun GrapplingGun;

    private bool grappled;

    public Text energyDisplay;

    public GameObject player;
    private Battery battery;

    void Start()
    {
        battery = player.GetComponent<Battery>();
    }
    void Update()
    {
        energy = battery.GetBatteryCharge();
        energyDisplay.text = energy.ToString();
        /*
                if (GrapplingGun.isGrappling && energy > 0)
                {
                    energy--;
                }
                energyDisplay.text = energy.ToString();


             
        if (Input.GetMouseButton(1))
        {
            energy++;
        }
        */
    }
}
