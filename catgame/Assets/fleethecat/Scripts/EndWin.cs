using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            // Collider hat Trigger "MyObjectTag" betreten
            // f�ge hier deinen Code ein, um auf die Kollision zu reagieren
            SceneManager.LoadScene("winscreen");
        }
    }

}
