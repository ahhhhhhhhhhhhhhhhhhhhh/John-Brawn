using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    public int height;
    public int width;
    public int size = 1; //size of gridsquares

    public bool drawGrid;

    private Grid[,] grid; //represents game map grid, consisting of towers, environment, friendly buildings, etc

    // Use this for initialization
    void Start ()
    {
        grid = new Grid[width, height];
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnDrawGizmos()
    {
        if (drawGrid) { 
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 pos = new Vector3(i * size, j * size, 0);
                    Gizmos.DrawWireCube(pos, new Vector3(size, size, size));
                }
            }
        }
    }

    public void set(int x, int y, Grid input)
    {
        grid[x, y] = input;
    }

    public Grid get(int x, int y)
    {
        return grid[x, y];
    }
}
