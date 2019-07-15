using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    public int height;
    public int width;
    public int size = 1; //size of gridsquares

    public bool drawGrid;

    private Tile[,] background; //map background, only visual. Does not affect tower placement
    private Tile[,] buildingLayer; //represents game grid of towers, buildings, hazards. Affects tower placement

    // Use this for initialization
    void Start ()
    {
        background = new Tile[width, height];
        buildingLayer = new Tile[width, height];
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

    public void placeTower(int x, int y)
    {
        buildingLayer[x, y] = Tile.Tower;
    }

    public Tile get(int x, int y)
    {
        return buildingLayer[x, y];
    }

    public Tile getBackground(int x, int y)
    {
        return background[x, y];
    }
}
