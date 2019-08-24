using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

    [Header("Unity Setup")]
    public WaveController waveControl;
    public GridController gridControl;
    public Pathfinder pathfinder;
    public Text moneyDisplay;

    private int cash;

    public void loadLevel(LevelInfo level)
    {
        waveControl.waves = level.waves;
        waveControl.spawningDirections = level.spawningDirections;

        gridControl.deleteGrid();
        gridControl.backgroundMap = level.backgroundMap;
        gridControl.buildingMap = level.buildingMap;
        gridControl.initLayers();

        pathfinder.endpoint = level.endpoint;

        cash = 20000;
    }
    public int getMoney()
    {
        return cash;
    }

    public void addMoney(int moreCash)
    {
        cash += moreCash;
    }

    public void subtractMoney(int costlyCosts)
    {
        cash -= costlyCosts;
    }

    public void setMoney(int money)
    {
        cash = money;
    }

    void Update()
    {
        moneyDisplay.text = (cash + "");
    }
}
