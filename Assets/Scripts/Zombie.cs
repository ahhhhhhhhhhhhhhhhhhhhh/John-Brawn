﻿using System.Collections;
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
    public Sprite[] sprites; //up, right, down, left

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
        target = Waypoints.points[waypointIndex];

        spriteRenderer = GetComponent<SpriteRenderer>();
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

        //makes zombie face direction it is moving
        Vector3 diff = target.position - transform.position;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if (diff.x > 0)
            {
                spriteRenderer.sprite = sprites[1]; //right
            }
            else
            {
                spriteRenderer.sprite = sprites[3]; //left
            }
        }
        else
        {
            if (diff.y > 0)
            {
                spriteRenderer.sprite = sprites[0]; //up
            }
            else
            {
                spriteRenderer.sprite = sprites[2]; //down
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        checkWaypoint();
	}

    public void checkWaypoint()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.1)
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
            else
            {
                target = Waypoints.points[waypointIndex];
            }
        }
    }

    public void hit(float damage)
    {
        health -= damage;
    }
}
