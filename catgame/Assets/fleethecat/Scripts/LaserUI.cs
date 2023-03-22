using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserUI : MonoBehaviour
{

    Graphic m_Graphic;
    Color m_MyColor;
    public KeyCode activationKey = KeyCode.E;
    public double fadingTime = 0.1;
    public Color normalColor = Color.green;
    public Color laserOnColor = Color.red;
    public Color laserOnColor2 = Color.magenta;
    public Color coolDownColor = Color.grey;

    // Laser cooldown durations
    public float laserDuration = 10f; // Max time the laser is on
    public float reloadDuration = 10f; // Time the laser needs to recharge

    // Laser state variables
    public bool laserOn = false; // Is the laser on or not
    [SerializeField] private float laserTime = 0f; // Current time of current state (either on or off)

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Graphic from the GameObject
        m_Graphic = GetComponent<Graphic>();
        //Create a new Color that starts as red
        m_MyColor = normalColor;
        //Change the Graphic Color to the new Color
        m_Graphic.color = m_MyColor;
    }

    // Update is called once per frame
    void Update()
    {

        laserTime -= Time.deltaTime; // reduce countdown

        // If activation key is pressed, the laser is not currently on, and the cooldown is over
        if (Input.GetKeyDown(activationKey) && !laserOn && laserTime <= 0)
        {
            // Turn the laser on and start the timer for how long it should last
            laserOn = true;
            laserTime = laserDuration;
        }

        if (laserOn)
        {
            if (laserTime > 0)
            {
                m_MyColor = Color.Lerp(laserOnColor, laserOnColor2, Mathf.PingPong(Time.time, 1));  // blinking 
            }
            else
            {
                laserOn = false;
                laserTime = reloadDuration; // Start the reload timer
            }
        }
        else
        {
            if (laserTime > 0)
            {
                m_MyColor = coolDownColor;  // cool down phase
            }
            else
            {
                m_MyColor = normalColor;
            }
        }

        m_Graphic.color = m_MyColor;

    }
}
