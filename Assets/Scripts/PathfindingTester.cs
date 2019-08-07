using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//just a simple object to visualize path calculation
public class PathfindingTester : MonoBehaviour {

    public float iconsize;

    public bool drawPath;

    private Pathfinder pathfinder;
    private Path path;

    private Transform endpointObject;
    private Vector2Int endpoint;

	// Use this for initialization
	void Start ()
    {
        pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();

        endpointObject = GameObject.Find("Endpoint").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        endpoint = new Vector2Int((int)endpointObject.position.x, (int)endpointObject.position.y);

		if (path == null || transform.hasChanged || endpointObject.hasChanged)
        {
            path = pathfinder.getPath(transform, endpoint);
        }
	}

    private void OnDrawGizmos()
    {
        if (drawPath && path != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.nodes.Count - 1; i++)
            {
                Node current = (Node)path.nodes[i];
                Node next = (Node)path.nodes[i + 1];
                Gizmos.DrawLine(current.gameObject.transform.position, next.gameObject.transform.position);
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, iconsize);
    }
}
