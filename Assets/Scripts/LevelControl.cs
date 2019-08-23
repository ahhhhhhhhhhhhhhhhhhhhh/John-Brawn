using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {

    [Header("Unity Setup")]
    public WaveController waveControl;
    public GridController gridControl;
    public Pathfinder pathfinder;

    public void loadLevel(LevelInfo level)
    {
        waveControl.waves = level.waves;
        waveControl.spawningDirections = level.spawningDirections;

        gridControl.deleteGrid();
        gridControl.backgroundMap = level.backgroundMap;
        gridControl.buildingMap = level.buildingMap;
        gridControl.initLayers();

        pathfinder.endpoint = level.endpoint;
    }
}
