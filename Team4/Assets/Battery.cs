using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    private float batteryCharge = 0f;
    private float batteryCapasity = 100f;
    private bool batteryFull;
    private bool isCharging = false;
    private bool isDraining = false;
    [SerializeField]
    public Color emissiveColor = Color.green;

    [SerializeField]
    protected bool isPlayer = false;
    [SerializeField]
    protected float chargeSpeed = 10;
    private float moveValue = 1;
    Renderer rend;


    //
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("Color_e3c93b9dc3f84582b541623a8917c33d", emissiveColor);
    }

    void Update()
    {
        chargeBattery(chargeSpeed);
        DrainBattery(chargeSpeed);
        rend.material.SetFloat("Vector1_6ca2df4b285a457aa45700fc5b3784a5", batteryCharge / 4);
    }

    public float GetBatteryCharge()
    {
        return batteryCharge;
    }
    public float GetMoveValue()
    {
        return moveValue;
    }
    public bool GetBatteryFull()
    {
        return batteryFull;
    }

    public bool GetBatteryChargeLeft(float value)
    {
        if (batteryCharge >= value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void chargeBattery(float amount)
    {
        if (isCharging)
        {
            if (batteryCharge <= batteryCapasity)
            {
                batteryFull = false;
                batteryCharge += Time.deltaTime * chargeSpeed;
                Debug.Log("Battery charging: " + batteryCharge);
            }
            else
            {
                Debug.Log("Battery full");
                batteryFull = true;
                batteryCharge = batteryCapasity;
            }
        }

    }

    public void DrainBattery(float amount)
    {
        if (isDraining)
        {
            batteryFull = false;
            if (batteryCharge >= 0f)
            {
                batteryCharge -= Time.deltaTime * chargeSpeed;
                Debug.Log("Battery draining: " + batteryCharge);
            }
            else
            {
                batteryCharge = 0f;
                StopDrain();
            }
        }
    }
    public void StopCharge()
    {
        Debug.Log("Chargu stoppu");

        isCharging = false;
    }
    public void StartCharge()
    {
        Debug.Log("Chargu staatto");

        isCharging = true;
    }
    public void StopDrain()
    {
        Debug.Log("Drainu stoppu");

        isDraining = false;
    }
    public void StartDrain()
    {
        Debug.Log("Drainu staatto");

        isDraining = true;
    }

}
