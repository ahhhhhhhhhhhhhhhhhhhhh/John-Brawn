﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTowerButton : MonoBehaviour {

    public GameObject towerPrefab; //tower this button will buy
    private Tower tower;

    private LevelData player;
    private BuildingManager buildManager;

	// Use this for initialization
	void Start ()
    {
        GameObject levelControl = GameObject.Find("Level Control");
        player = levelControl.GetComponent<LevelData>();
        buildManager = levelControl.GetComponent<BuildingManager>();

        tower = towerPrefab.GetComponent<Tower>();

        Text childText = transform.GetChild(0).gameObject.GetComponent<Text>();
        childText.text = "Buy " + tower.name + " ($" + tower.properties[0].cost + ")";

        GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(setTowerToBuild));
    }

    private void Update()
    {
        if (player.money < tower.properties[0].cost)
        {
            GetComponent<Image>().color = Color.red;
            GetComponent<Button>().enabled = false;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponent<Button>().enabled = true;
        }
    }

    private void setTowerToBuild()
    {
        buildManager.setTowerToBuild(towerPrefab);
    }
}
