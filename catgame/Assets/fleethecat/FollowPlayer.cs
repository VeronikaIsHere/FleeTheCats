using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;  // player
    public int inRangeDistance;  // when the cat should notice the player
    NavMeshAgent nav;
   

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance (transform.position, target.transform.position) < inRangeDistance)  // check distance
        {
            nav.SetDestination(target.position);  // go to position of player 
        } 
        else
        {
            nav.destination = transform.position;  // stop following 
        }
        
    }
}
