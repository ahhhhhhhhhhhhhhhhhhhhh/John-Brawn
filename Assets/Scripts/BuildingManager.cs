using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {

    [HideInInspector]
    public bool buildingMode;
    private GameObject towerToBuild;
    private GameObject selectedTower;

    [Header("Tower Types")]
    public GameObject basicTowerPrefab;
    public GameObject fastTowerPrefab;

    [Header("Unity Setup Stuff")]
    public GridController grid;
    public SelectCursor cursor;
    public GameObject towerInfoPanel;
    public Pathfinder pathfinder;
    public Enemies enemies;
    public MoneyManager moneyManager;

    // Use this for initialization
    void Start ()
    {
        closeInfoPanel();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (buildingMode)
        {
            closeInfoPanel();
        }

		if (buildingMode && Input.GetMouseButtonDown(0))
        {
            Vector3 roundedPos = cursor.getRoundedPos();
            int cost = towerToBuild.GetComponent<Tower>().getProperties().cost;
            if (canBuildTower(roundedPos) && cost <= moneyManager.money)
            {
                BuildTower(roundedPos);
                if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    BuildingModeOff();
                }
            }
        }

        if (buildingMode && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)))
        {
            BuildingModeOff();
        }
	}

    private void BuildTower(Vector3 roundedPos)
    {
        grid.placeTower((int)roundedPos.x, (int)roundedPos.y);
        Instantiate(towerToBuild, roundedPos, new Quaternion(0, 0, 0, 0));

        int cost = towerToBuild.GetComponent<Tower>().getProperties().cost;
        moneyManager.subtract(cost);

        updatePaths();
    }

    public bool canBuildTower(Vector3 roundedPos)
    {
        try
        { //sometimes the mouse is out of bounds of the tilemap
            Tile tile = grid.get((int)roundedPos.x, (int)roundedPos.y);
            List<GameObject> enemiesInSquare = enemies.getEnemies((int)roundedPos.x, (int)roundedPos.y); //can't place a tower on top of enemies

            return tile == 0 && enemiesInSquare.Count == 0;
        }
        catch
        { //so if the mouse is out of the map you definitely can't build there
            return false;
        }  
    }

    public void BuildingModeOff()
    {
        buildingMode = false;
    }

    public void setTowerToBuild(GameObject tower)
    {
        buildingMode = true;
        towerToBuild = tower;
    }

    public GameObject getTowerToBuild()
    {
        return towerToBuild;
    }

    public void selectTower(GameObject tower)
    {
        if (!buildingMode)
        {
            selectedTower = tower;
            towerInfoPanel.GetComponent<TowerInfoPanel>().setSelectedTower(selectedTower.GetComponent<Tower>());
            towerInfoPanel.GetComponent<TowerInfoPanel>().loadTowerInfo();
            towerInfoPanel.SetActive(true);
        }
    }

    public GameObject getSelectedTower()
    {
        return selectedTower;
    }

    public void closeInfoPanel()
    {
        towerInfoPanel.SetActive(false);
        selectedTower = null;
    }

    public void upgradeSelectedTower()
    {
        Tower tower = selectedTower.GetComponent<Tower>();
        TowerInfo next = tower.properties[tower.getLevel() + 1];
        int cost = next.cost;

        if (moneyManager.money >= cost)
        {
            selectedTower.GetComponent<Tower>().upgrade();
            towerInfoPanel.GetComponent<TowerInfoPanel>().loadTowerInfo();
            moneyManager.subtract(cost);
        }
    }

    public void sellSelectedTower()
    {
        grid.removeTower((int)selectedTower.transform.position.x, (int)selectedTower.transform.position.y);
        selectedTower.GetComponent<Tower>().sell();
        closeInfoPanel();

        updatePaths();
    }

    private void updatePaths()
    {
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        pathfinder.OnGridChange();
        Debug.Log("Total repath calculation: " + (watch.ElapsedMilliseconds) + " ms");
    }
}
