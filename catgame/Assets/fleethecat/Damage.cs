using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public PlayerHealth pHealth;
    public int damageRange;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pHealth.transform.position) < damageRange)  // check distance
        {
            pHealth.health -= damage;
        }
    }

}
