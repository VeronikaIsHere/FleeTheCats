using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public Slider healthBar;
    public int damageDistance;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBar = (Slider)FindObjectOfType(typeof(Slider));

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}