using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public float iconSize = 0.2f; 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(transform.position, iconSize);
    }
}
