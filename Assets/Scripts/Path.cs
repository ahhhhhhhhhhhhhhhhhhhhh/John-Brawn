using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

    public ArrayList nodes;

    public bool draw;
	
    public Path()
    {
        nodes = new ArrayList();

        draw = false;
    }

    public Path(Node node) : this()
    {
        nodes.Add(node);
    }

    public int Size()
    {
        return nodes.Count;
    }

    public void appendNode(Node node)
    {
        nodes.Add(node);
    }

    public void preppendNode(Node node)
    {
        nodes.Insert(0, node);
    }

    public override string ToString()
    {
        string str = "";
        foreach (Node node in nodes)
        {
            str += node.ToString() + "->";
        }
        return str;
    }
}
