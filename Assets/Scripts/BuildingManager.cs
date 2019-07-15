using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    [HideInInspector]
    public bool buildingMode;
    private GameObject towerToBuild;

    public SelectCursor cursor;

    [Header("Tower Types")]
    public GameObject basicTowerPrefab;
    public GameObject fastTowerPrefab;

    [Header("Unity Setup Stuff")]
    public GridController grid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (buildingMode && Input.GetMouseButtonDown(0))
        {
            BuildTower();
        }
	}

    private void BuildTower()
    {
        Vector3 roundedPos = cursor.getRoundedPos();
        if (grid.get((int)roundedPos.x, (int)roundedPos.y) != Tile.Tower)
        {
            grid.placeTower((int)roundedPos.x, (int)roundedPos.y);
            Instantiate(towerToBuild, roundedPos, new Quaternion(0, 0, 0, 0));
            buildingMode = false;
        }
    }

    public void BuildingModeOff()
    {
        buildingMode = false;
    }

    public void setBasicTower()
    {
        buildingMode = true;
        towerToBuild = basicTowerPrefab;
    }

    public void setFastTower()
    {
        buildingMode = true;
        towerToBuild = fastTowerPrefab;
    }
}
