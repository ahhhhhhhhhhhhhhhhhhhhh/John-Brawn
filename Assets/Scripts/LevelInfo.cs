using UnityEngine;

[System.Serializable]
public class LevelInfo {
    public Texture2D backgroundMap;
    public Texture2D buildingMap;
    public Vector2Int endpoint;
    public WaveInfo[] waves;
    public WaveController.Direction[] spawningDirections;
    public int startingMoney;
    //other level data
}
