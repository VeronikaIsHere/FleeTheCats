using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class LaserDestination : MonoBehaviour
{
    public Vector3 targetPosition;
    private GazePoint gazePoint;
    [SerializeField] private int pointSize = 50;
    [SerializeField] private Ray ray;
    public LineRenderer lineRenderer;
    public GameObject player;
    public KeyCode activationKey = KeyCode.E;

    // Laser cooldown durations
    public float laserDuration = 10f; // Max time the laser is on
    public float reloadDuration = 10f; // Time the laser needs to recharge

    // Laser state variables
    public bool laserOn = false; // Is the laser on or not
    [SerializeField] private float laserTime = 0f; // Current time of current state (either on or off)

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
                lineRenderer.enabled = true; // Make laser Visible
                ActivateLaser();
            }
            else
            {
                lineRenderer.enabled = false; // Make laser Invisible
                laserOn = false;
                laserTime = reloadDuration; // Start the reload timer
            }
        }
    }

    // Method to visualize the target position in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(targetPosition, 1); //gazepoint in sceneview visulazied
    }

    // Activate the laser
    private void ActivateLaser()
    {
        gazePoint = TobiiAPI.GetGazePoint();

        if (gazePoint.IsValid)
        {
            RaycastHit hit;

            targetPosition = gazePoint.Screen;
            ray = Camera.main.ScreenPointToRay(targetPosition);

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }

            lineRenderer.SetPositions(new Vector3[] { targetPosition, player.transform.position });
        }
    }
}
