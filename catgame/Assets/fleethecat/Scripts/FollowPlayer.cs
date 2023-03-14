using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;  // player
    public int inRangeDistance;  // when the cat should notice the player
    NavMeshAgent nav;
    private GazeAware _gazeAware;
    private int catCooldown = 0;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _gazeAware = GetComponent<GazeAware>();
    }

    // Update is called once per frame
    void Update()
    {
        if (catCooldown-- <= 0 && Vector3.Distance(transform.position, target.transform.position) < inRangeDistance) // check cooldown and distance
        {
            if (_gazeAware.HasGazeFocus) //check if gaze on cat
            {
                catCooldown = 100;
                nav.destination = transform.position; //dont move
            }
            else
            {
                nav.SetDestination(target.position);  // go to position of player
            }
        }
        else
        {
            nav.destination = transform.position;  // stop following 
        }

        

    }
}
