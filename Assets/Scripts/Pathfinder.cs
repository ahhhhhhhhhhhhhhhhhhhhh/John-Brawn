using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [Header("Debug")]
    public bool drawNodes;
    public float nodeIconSize;

    [Header("Unity Setup")]
    public GridController grid;

    public GameObject nodePrefab;

    [HideInInspector]
    public Vector2Int endpoint;

    private Node[,] nodes;
    private bool firstSearchComplete;

    private Transform enemies; //parent of all enemies

	// Use this for initialization
	void Start ()
    { 
        nodes = new Node[grid.getWidth(), grid.getHeight()];
        for (int x = 0; x < nodes.GetLength(0); x++)
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                Node node = Instantiate(nodePrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0)).GetComponent<Node>();
                node.gameObject.transform.parent = transform;
                nodes[x, y] = node;
            }
        }

        firstSearchComplete = false;

        enemies = GameObject.Find("Enemies").transform;
    }

    //does a bredth first search through nodes to find all paths to endpoint
    public void Search()
    {
        Node endNode = nodes[endpoint.x, endpoint.y];
        endNode.cost = 0;

        List<Node> frontier = new List<Node>();
        frontier.Add(endNode);

        List<Node> visited = new List<Node>();
        visited.Add(endNode);

        while (frontier.Count > 0)
        {
            Node current = frontier[0];
            frontier.RemoveAt(0);

            foreach (Node next in getNeighbors(current))
            {
                float nextStepCost = current.cost + getCost(current.x, current.y, next.x, next.y);

                if (!visited.Contains(next))
                {
                    frontier.Add(next);
                    visited.Add(next);

                    next.heuristic = calcHeuristic(next.gameObject.transform, endpoint);
                    next.cost = nextStepCost;
                    next.prevNode = current;
                }
                else
                {
                    if (nextStepCost < next.cost)
                    {
                        next.cost = nextStepCost;
                        next.prevNode = current;
                    }
                }
            }
        }

        firstSearchComplete = true;
    }

    //should be called everytime the grid is changed (ie a tower being placed or destroyed
    public void OnGridChange()
    {
        for (int x = 0; x < nodes.GetLength(0); x++)
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                nodes[x, y].prevNode = null;
            }
        }
        Search();
        for (int i = 0; i < enemies.childCount; i++)
        {
            enemies.GetChild(i).gameObject.GetComponent<Zombie>().repath();
        }
    }

    //returns the next step in the path from the given position
    public Transform getNextNode(Transform pos)
    {
        if (!firstSearchComplete)
        {
            Search();
        }
        Node current = findNearestNode(pos);
        return current.prevNode.gameObject.transform;
    }

    private List<Node> getNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue; //skips rest of code but continues loop
                }

                int testX = node.x + x;
                int testY = node.y + y;

                try //may be negative or off the grid
                {
                    if (grid.isWalkable(testX, testY))
                    {
                        neighbors.Add(nodes[testX, testY]);
                    }
                }
                catch { }
            }
        }

        return neighbors;
    }

    private float calcHeuristic(Transform node, Vector2Int destination)
    {
        float dx = destination.x - node.position.x;
        float dy = destination.y - node.position.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    private float getCost(int startX, int startY, int endX, int endY)
    {
        float taxiDist = Mathf.Abs(startX - endX) + Mathf.Abs(startY - endY);
        if (taxiDist == 0)
        {
            return 0;
        }
        else if (taxiDist == 1)
        {
            return 1f;
        }
        else if (taxiDist == 2) //diagonal movement is a bit more expensive so enemies don't move diagonally when they should go straight
        {
            return 1.4f;
        }
        else //shouldn't ever get to this point, costs should only be for adjacent squares
        {
            throw new System.Exception("tried to get cost between non-adjacent nodes");
        }
    }

    private Node findNearestNode(Transform loc)
    {
        float minDist = float.MaxValue;
        Node nearest = null;
        if (loc.position.x < -0.5f)
        {
            for (int i = 0; i < nodes.GetLength(1); i++)
            {
                Node test = nodes[0, i];
                float dist = Vector3.Distance(loc.position, test.gameObject.transform.position);
                if (dist < minDist && grid.isWalkable(0, i))
                {
                    minDist = dist;
                    nearest = test;
                }
            }
        }
        else if (loc.position.x > nodes.GetLength(0) - 0.5f)
        {
            for (int i = 0; i < nodes.GetLength(1); i++)
            {
                Node test = nodes[nodes.GetLength(0) - 1, i];
                float dist = Vector3.Distance(loc.position, test.gameObject.transform.position);
                if (dist < minDist && grid.isWalkable(nodes.GetLength(0) - 1, i))
                {
                    minDist = dist;
                    nearest = test;
                }
            }
        }
        else if (loc.position.y < -0.5f)
        {
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                Node test = nodes[i, 0];
                float dist = Vector3.Distance(loc.position, test.gameObject.transform.position);
                if (dist < minDist && grid.isWalkable(i, 0))
                {
                    minDist = dist;
                    nearest = test;
                }
            }
        }
        else if (loc.position.y > nodes.GetLength(1) - 0.5f)
        {
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                Node test = nodes[i, nodes.GetLength(1) - 1];
                float dist = Vector3.Distance(loc.position, test.gameObject.transform.position);
                if (dist < minDist && grid.isWalkable(i, nodes.GetLength(1) - 1))
                {
                    minDist = dist;
                    nearest = test;
                }
            }
        }
        else
        {
            try
            {
                nearest = nodes[Mathf.RoundToInt(loc.position.x), Mathf.RoundToInt(loc.position.y)];
            } catch { }
        }
        return nearest;
    }
}
