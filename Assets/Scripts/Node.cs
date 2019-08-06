using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, System.IComparable<System.Object>
{
    public float iconSize = 0.2f;

    [HideInInspector]
    public float heuristic;
    [HideInInspector]
    public Node prevNode;
    [HideInInspector]
    public int depth;
    [HideInInspector]
    public float cost;

    private void OnDrawGizmos()
    {
        if (transform.parent.gameObject.GetComponent<Pathfinder>().drawNodes)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawSphere(transform.position, iconSize);
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
