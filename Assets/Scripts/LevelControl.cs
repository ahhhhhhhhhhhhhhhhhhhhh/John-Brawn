using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

    [Header("Unity Setup")]
    public Pathfinder pathfinder;

    [HideInInspector()]
    public LevelInfo loadedLevel;

    public void loadLevel(LevelInfo level)
    {
        WaveController waveControl = GetComponent<WaveController>();
        GridController gridControl = GetComponent<GridController>();
        LevelData player = GetComponent<LevelData>();

        waveControl.waves = level.waves;
        waveControl.spawningDirections = level.spawningDirections;

        gridControl.deleteGrid();
        gridControl.backgroundMap = level.backgroundMap;
        gridControl.buildingMap = level.buildingMap;
        gridControl.initLayers();

        player.addMoney(level.startingMoney);
        player.addLives(level.startingLives);

        pathfinder.endpoint = level.endpoint;

        loadedLevel = level;
    }
}
