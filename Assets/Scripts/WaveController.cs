using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public bool on = true; //way to turn wave spawning off for testing purposes

    [Header("Wave Properties")]
    public float timeBetweenWaves = 3f;
    private float timer = 1f;
    private int waveNum = 0;

    [Header("Unity Setup Stuff (don't change)")]
    public Transform enemies; //parent of all enemies
    public GameObject enemyPrefab;

    //temp, would like to have multiple spawnpoints/more flexible system
    public Transform spawnpoint;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= 0 && on)
        {
            SpawnWave();
            waveNum += 1;
            timer = timeBetweenWaves;
        }

        timer -= Time.deltaTime;
	}

    void SpawnWave()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
        enemy.transform.parent = enemies;
    }
}
