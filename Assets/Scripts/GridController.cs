﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridController : MonoBehaviour
{

    public int height;
    public int width;
    public int size = 1; //size of gridsquares 

    public bool drawGridLines;

    [Header("Tiles")]
    public GameObject[] tiles; //IMPORTANT: tiles must be in the same order as Tile enum (not including Tile.tower)
    public Texture2D map;
    public ColorToTile[] colorMappings;

    private Tile[,] background; //map background, only visual. Does not affect tower placement
    private Tile[,] buildingLayer; //represents game grid of towers, buildings, hazards. Affects tower placement

    // Use this for initialization
    void Start()
    {
        background = new Tile[width, height];
        buildingLayer = new Tile[width, height];

        //testing
        /*
        background[2, 5] = Tile.Sand;
        background[6, 7] = Tile.Concrete;
        background[7, 7] = Tile.Concrete;
        background[6, 6] = Tile.Concrete;
        background[7, 6] = Tile.Concrete;
        buildingLayer[8, 3] = Tile.House;
        buildingLayer[7, 3] = Tile.House;
        */


        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                Color color = map.GetPixel(x, y);
                //InstantiateThing(x, y, color);
                foreach (ColorToTile colorMapping in colorMappings)
                {
                    if (color.g == colorMapping.color.g)
                    {
                        background[x, y] = colorMapping.tile;
                    }
                    if (color.b == colorMapping.color.b)
                    {
                        buildingLayer[x, y] = colorMapping.tile;
                    }
                }

            }
        }

        drawGrid();
    }

    public void drawGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = new Vector3(i * size, j * size, 0);

                //instantiating background tile
                GameObject tile = Instantiate(tiles[(int)background[i, j]]);
                TileInfo tileInfo = tile.GetComponent<TileInfo>();
                tile.transform.position = pos;
                if (tileInfo.rotatable)
                {
                    tile.transform.Rotate(new Vector3(0, 0, 90) * Random.Range(0, 5));
                }

                //instantiating buildings (if any)
                if (buildingLayer[i, j] != 0)
                {
                    GameObject building = Instantiate(tiles[(int)buildingLayer[i, j]]);
                    building.transform.position = pos;
                }
            }
        }
    }
 
    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (drawGridLines)
        {
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
