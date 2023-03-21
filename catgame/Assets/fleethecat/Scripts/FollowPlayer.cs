using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;  // player
    public int inRangeDistance;  // when the cat should notice the player
    public int catCooldown = 200; // max cooldown for cats (can be made public or serialized if prefeb)

    private NavMeshAgent nav;
    private GazeAware _gazeAware; // use the gaze
    private int currentCooldown = 0; // current cooldown (can be < 0)

    private LaserDestination laserPoint;
    public GameObject laserObject;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _gazeAware = GetComponent<GazeAware>();
        laserPoint = laserObject.GetComponent<LaserDestination>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log(laserPoint.targetPosition.x + "->" + laserPoint.targetPosition.y + "->" + laserPoint.targetPosition.z + "->" + laserPoint.ToString());
        if (laserPoint != null && laserPoint.laserOn && Vector3.Distance(transform.position, laserPoint.targetPosition) < inRangeDistance)
        {
            nav.SetDestination(laserPoint.targetPosition);
        }
        else if (currentCooldown-- <= 0 && Vector3.Distance(transform.position, target.transform.position) < inRangeDistance) // check cooldown and distance
        {
            if (_gazeAware.HasGazeFocus) // check if gaze on cat
            {
                currentCooldown = catCooldown;
                nav.destination = transform.position; // don't move
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
    void OnDrawGizmos() // wire sphere to see where range of cat is and if plaver is in it
    {
        Gizmos.color = Color.green;
        if(laserPoint != null && laserPoint.laserOn && Vector3.Distance(transform.position, laserPoint.targetPosition) < inRangeDistance ) //if laserpoint is not null and on and in range of cat set color yellow
        {
            Gizmos.color = Color.yellow;
        }
        else if (Vector3.Distance(transform.position, target.transform.position) < inRangeDistance) //if player in range set red 
        {
            Gizmos.color = Color.red;
            if(_gazeAware.HasGazeFocus) //if player is looking at cat set orange
            {
                Gizmos.color = Color.yellow;
            }

        }
        Gizmos.DrawWireSphere(transform.position, inRangeDistance);

        if (laserPoint != null && laserPoint.laserOn) //if laser is not null and on 
        {
            Gizmos.DrawWireSphere(laserPoint.targetPosition, .5f); //gazepoint in sceneview visulazied
        }


    }
}
