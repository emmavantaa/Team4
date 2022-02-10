using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private bool isDraining = false;
    [SerializeField]
    private bool isInfinite = true;
    private bool isEmpty = false;
    [SerializeField]
    public float chargeAmount = 100;
    [SerializeField]
    public float emissiveIntensity = 10;
    [SerializeField]
    public Color emissiveColor = Color.yellow;

    [SerializeField]
    private float energyCharge = 100;
    private Battery playerBattery;
    Renderer rend;

    // Start is called before the first frame update

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("Color_e3c93b9dc3f84582b541623a8917c33d", emissiveColor);

        //rend.material.shader = Shader.Find("Multiply");
    }

    // Update is called once per frame
    void Update()
    {
        DrainGenerator();
        // float shininess = Mathf.PingPong(Time.time, 1.0f);
        rend.material.SetFloat("Vector1_6ca2df4b285a457aa45700fc5b3784a5", emissiveIntensity);
    }

    public void DrainGenerator()
    {
        if (isDraining && playerBattery != null)
        {
            if (isInfinite)
            {
                playerBattery.StartCharge();
            }
            else if (energyCharge >= chargeAmount)
            {
                playerBattery.StartCharge();
                energyCharge -= Time.deltaTime * chargeAmount;
                emissiveIntensity = energyCharge / 10;
                Debug.Log(energyCharge);

            }
            else
            {
                playerBattery.StopCharge();
                energyCharge = 0;
                // Debug.Log(Generat);
                isEmpty = true;
                isDraining = false;
            }
        }
    }

    public void StartDrain(Battery setPlayerBattery)
    {
        isDraining = true;
        playerBattery = setPlayerBattery;
    }
    public void StopDrain()
    {
        playerBattery.StopCharge();
        Debug.Log("Stop Drain");
        playerBattery = null;
        isDraining = false;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }
}
