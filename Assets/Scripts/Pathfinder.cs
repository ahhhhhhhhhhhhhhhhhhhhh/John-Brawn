using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    public bool drawNodes;

    public int maxSearchDepth; //max number of steps the algorithm will search for a path

    [Header("Unity Setup")]
    public GridController grid;

    public GameObject nodePrefab;

    private ArrayList open;
    private ArrayList closed;

    private Node[,] nodes;

	// Use this for initialization
	void Start ()
    {
        open = new ArrayList();
        closed = new ArrayList();

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
	}
	
	public Path getPath(Transform mover, Vector2Int destination)
    {
        open.Clear();
        closed.Clear();

        Node target = nodes[destination.x, destination.y];
        target.prevNode = null;

        Node start = findNearestNode(mover);
        start.cost = 0;
        open.Add(start);

        if (start == target)
        {
            return new Path(target);
        }

        int maxDepth = 0;
        while (maxDepth < maxSearchDepth && open.Count > 0)
        {
            Node current = (Node)open[0];
            if (current == nodes[destination.x, destination.y]) //means we have reached target
            {
                break;
            }

            open.Remove(current);
            addClosed(current);

            //loop through all adjacent nodes
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue; //skips rest of code but continues loop
                    }

                    int testX = (int)current.gameObject.transform.position.x + x;
                    int testY = (int)current.gameObject.transform.position.y + y;

                    if (testX >= 0 && testX < grid.getWidth() && testY >= 0 && testY < grid.getHeight() && grid.get(testX, testY) == (Tile)0)
                    {
                        Node adjNode = nodes[testX, testY];
                        adjNode.heuristic = calcHeuristic(adjNode, destination);

                        float nextStepCost = current.cost + 1;

                        //if we previously searched this node but have now found a faster path we update it
                        if (nextStepCost < adjNode.cost) 
                        {
                            if (open.Contains(adjNode))
                            {
                                open.Remove(adjNode);
                            }
                            if (closed.Contains(adjNode))
                            {
                                closed.Remove(adjNode);
                            }
                        }

                        if (!open.Contains(adjNode) && !closed.Contains(adjNode))
                        {
                            adjNode.cost = nextStepCost;
                            adjNode.prevNode = current;
                            maxDepth = Mathf.Max(maxDepth, adjNode.depth);
                            open.Add(adjNode);
                        }
                    }
                }
            }
        }

        if (target.prevNode == null) { //no path as found
            return null;
        }

        Path path = new Path();
        while (target != start)
        {
            path.preppendNode(target);
            target = target.prevNode;
        }
        path.preppendNode(start);

        return path;
    }

    private float calcHeuristic(Node node, Vector2Int destination)
    {
        float dx = destination.x - node.gameObject.transform.position.x;
        float dy = destination.y - node.gameObject.transform.position.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    //we want closed to always be sorted, so just add using this method to make sure it is always sorted
    private void addClosed(Node node)
    {
        closed.Add(node);
        closed.Sort();
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
                if (dist < minDist)
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
                if (dist < minDist)
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
                if (dist < minDist)
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
                if (dist < minDist)
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
