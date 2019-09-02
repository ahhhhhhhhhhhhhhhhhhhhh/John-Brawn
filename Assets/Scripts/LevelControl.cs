using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

    [Header("Unity Setup")]
    public WaveController waveControl;
    public GridController gridControl;
    public Pathfinder pathfinder;
    public PlayerData player;

    public void loadLevel(LevelInfo level)
    {
        waveControl.waves = level.waves;
        waveControl.spawningDirections = level.spawningDirections;

        gridControl.deleteGrid();
        gridControl.backgroundMap = level.backgroundMap;
        gridControl.buildingMap = level.buildingMap;
        gridControl.initLayers();

        pathfinder.endpoint = level.endpoint;

        player.addMoney(level.startingMoney);
        player.addLives(level.startingLives);
    }
}
