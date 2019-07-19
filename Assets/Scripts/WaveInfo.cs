using UnityEngine;

[System.Serializable]
public class WaveInfo {
    public float timeBetweenWaves;
    public float initialDelay;
    public float spawnDelay = 0.1f; //time between each spawn within a wave
    public int[] wavePattern; //number of enemies to spawn on each wave (waveNum = index)
}
