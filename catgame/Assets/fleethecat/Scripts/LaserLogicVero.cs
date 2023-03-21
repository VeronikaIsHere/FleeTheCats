using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class LaserLogicVero : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform laserSpawn;
    public float laserDistance = 50f;
    public KeyCode activationKey = KeyCode.E;

    private bool isLaserActive = false;
    private GameObject laser;
    private Vector3 targetPosition;

    private void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            isLaserActive = !isLaserActive;

            if (isLaserActive)
            {
                laser = Instantiate(laserPrefab, laserSpawn.position, Quaternion.identity);
            }
            else
            {
                Destroy(laser);
            }
        }

        if (isLaserActive)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();

            if (gazePoint.IsValid)
            {
                targetPosition = gazePoint.Screen;
                targetPosition.z = laserDistance;
                targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);

                laser.transform.LookAt(targetPosition);
                laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, Vector3.Distance(laserSpawn.position, targetPosition));
                laser.transform.position = laserSpawn.position;
            }
        }
    }
}
