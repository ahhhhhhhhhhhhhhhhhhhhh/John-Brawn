using UnityEngine;

[System.Serializable]
public class LevelInfo {
    public Texture2D backgroundMap;
    public Texture2D buildingMap;
    public WaveInfo[] waves;
    public WaveController.Direction[] spawningDirections;
    //other level data
}
