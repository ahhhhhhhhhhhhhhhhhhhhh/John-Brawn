using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour {

    [Header("Zombie Attributes")]
    public float speed = 2f;
    public float maxHealth = 100f;
    private float health;

    public bool loop; //makes zombies loop around the waypoints rather than disapearing at the final one

    private Transform target;
    private int waypointIndex = 0;

    [Header("Unity Setup Stuff")]
    public Image healthBar;
    public GameObject deathEffect;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
        target = Waypoints.points[waypointIndex];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            return;
        }

        healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);

        Vector3 targetPos = target.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) <= 0.1)
        {
            waypointIndex++;
            if (waypointIndex >= Waypoints.points.Length)
            {
                if (loop)
                {
                    waypointIndex = 0;
                    target = Waypoints.points[waypointIndex];
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else { 
                target = Waypoints.points[waypointIndex];
            }
        }
	}

    public void hit(float damage)
    {
        health -= damage;
    }
}
