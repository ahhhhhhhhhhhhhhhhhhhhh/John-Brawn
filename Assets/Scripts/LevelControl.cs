using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {

    [Header("Unity Setup")]
    public WaveController waveControl;
    public GridController gridControl;

    public void loadLevel(LevelInfo level)
    {
        waveControl.waves = level.waves;
        gridControl.backgroundMap = level.backgroundMap;
        gridControl.buildingMap = level.buildingMap;
    }
}
