using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, System.IComparable<System.Object>
{
    [HideInInspector]
    public float heuristic;
    [HideInInspector]
    public Node prevNode;
    [HideInInspector]
    public int depth;
    [HideInInspector]
    public float cost;
    
    public int x { get; private set; }
    public int y { get; private set; }

    private Pathfinder pathfinder;

    private void Start()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        pathfinder = transform.parent.gameObject.GetComponent<Pathfinder>();
    }

    private void OnDrawGizmos()
    {
        if (pathfinder.drawNodes)
        {
            if (prevNode == null)
            {
                Gizmos.color = Color.black;
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, prevNode.gameObject.transform.position);

                Gizmos.color = Color.grey;
            }
            Gizmos.DrawSphere(transform.position, pathfinder.nodeIconSize);
        }
    }

    public void setPrevNode(Node other)
    {
        prevNode = other;
        depth = other.depth + 1;
    }

    public int CompareTo(System.Object other)
    {
        if (other.GetType() == typeof(Node))
        {
            Node otherNode = (Node)other;
            if (this.heuristic < otherNode.heuristic)
            {
                return -1;
            }
            else if (this.heuristic > otherNode.heuristic)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public override string ToString()
    {
        return "[Node: " + transform.position.x + ", " + transform.position.y + "]";
    }
}
