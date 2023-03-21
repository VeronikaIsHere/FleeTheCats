using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject laserPrefab;
    public float laserDistance = 100f;

    private GameObject laser;
    private Vector3 laserOrigin;

    void Start()
    {
        // Instantiate the laser at the laser origin
        laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        // Get the position that the player is looking at, as determined by the Tobii Eye Tracker
        Vector3 lookPosition = TobiiAPI.GetGazePoint().Screen;

        // Convert the gaze point to world space
        lookPosition = mainCamera.ScreenToWorldPoint(new Vector3(lookPosition.x, lookPosition.y, mainCamera.nearClipPlane));

        // Cast a ray from the laser origin to the look position
        Ray ray = new Ray(transform.position, (lookPosition - transform.position).normalized);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, laserDistance))
        {
            // Set the position and direction of the laser to point towards the hit point
            laser.transform.LookAt(hit.point);

            // Set the length of the laser to the distance between the laser origin and the hit point
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, Vector3.Distance(laser.transform.position, hit.point));
        }
        else
        {
            // Set the position and direction of the laser to point towards the look position
            laser.transform.LookAt(lookPosition);

            // Set the length of the laser to the maximum distance
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, laserDistance);
        }
    }
}