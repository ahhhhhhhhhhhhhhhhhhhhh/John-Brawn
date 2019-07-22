using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Map Stuff")]
    public GameObject[] tiles; //IMPORTANT: tiles must be in the same order as Tile enum (not including Tile.tower)
    public ColorToTile[] colorMappings;
    public Texture2D backgroundMap;
    public Texture2D buildingMap;

    [Header("Unity Setup")]
    public Camera camera;

    private int size = 1; //size of gridsquares 
    private int height;
    private int width;

    private Tile[,] background; //map background, only visual. Does not affect tower placement
    private Tile[,] buildingLayer; //represents game grid of towers, buildings, hazards. Affects tower placement

    // Use this for initialization
    void Start()
    {
        initLayers();
        instantiateGrid();
    }

    public void initLayers()
    {
        height = backgroundMap.height;
        width = backgroundMap.width;
        camera.orthographicSize = height / 2f;
        camera.transform.position = new Vector3(height - 0.5f, height / 2f - 0.5f, -10);

        background = new Tile[width, height];
        buildingLayer = new Tile[width, height];

        addTiles(backgroundMap, background);
        addTiles(buildingMap, buildingLayer);
    }

    //goes through map pixel by pixel, matches color to Tile, and adds Tile to list
    public void addTiles(Texture2D map, Tile[,] layerList) {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                Color color = map.GetPixel(x, y);
                //InstantiateThing(x, y, color);
                foreach (ColorToTile colorMapping in colorMappings)
                {
                    if (colorMapping.color == color) {
                        layerList[x, y] = colorMapping.tile;
                    }
                }
            }
        }
    }

    //instantiates each tile in background and building layers
    public void instantiateGrid()
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
                tile.transform.parent = transform;

                //instantiating buildings (if any)
                if (buildingLayer[i, j] != 0)
                {
                    GameObject building = Instantiate(tiles[(int)buildingLayer[i, j]]);
                    building.transform.position = pos;
                    building.transform.parent = transform;
                }
            }
        }
    }

    //deletes all tile gameobjects. Does not modify background or buildingLayer variables
    public void deleteGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
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
