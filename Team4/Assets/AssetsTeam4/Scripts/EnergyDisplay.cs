using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{
    
    [SerializeField] private int energy; // This should eventually come from the player / manager holding the energy amount
    [SerializeField] GrapplingGun GrapplingGun;
    
    private bool grappled;

    public Text energyDisplay;

    void Update()
    {

        if (GrapplingGun.isGrappling && energy > 0)
        {
            energy--;
        }
        energyDisplay.text = energy.ToString();


        /** CHEAT - Get more energy with right mouse button */

        if (Input.GetMouseButton(1))
        {
            energy++;
        }

    }
}
