using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

    public float speed = 2;

    public bool loop; //makes zombies loop around the waypoints rather than disapearing at the final one

    private Transform target;
    private int waypointIndex = 0;

	// Use this for initialization
	void Start ()
    {
        target = Waypoints.points[waypointIndex];
	}
	
	// Update is called once per frame
	void Update ()
    {
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
}
