using UnityEngine;

[System.Serializable]
public class WaveInfo {
    public float delay; //time before wave starts
    public float spawnDelay = 0.1f; //time between each spawn within a wave

    public int numZombies;
    //other zombie types
}
