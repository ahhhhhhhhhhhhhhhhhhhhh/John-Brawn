using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public bool on = true; //way to turn wave spawning off for testing purposes

    [Header("Wave Properties")]
    public float timeBetweenWaves = 3f;
    public float initialDelay = 1f;
    public float spawnDelay = 0.1f; //time between each spawn within a wave
    public int[] wavePattern; //number of enemies to spawn on each wave (waveNum = index)

    private float timer;
    private int waveNum = 0;

    [Header("Unity Setup Stuff (don't change)")]
    public Transform enemies; //parent of all enemies
    public GameObject zombiePrefab;

    public Transform spawnpointParent;
    private Transform[] spawnpoints;

    // Use this for initialization
    void Start ()
    {
        timer = initialDelay;

        spawnpoints = new Transform[spawnpointParent.childCount];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = spawnpointParent.GetChild(i);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= 0 && on && waveNum < wavePattern.Length)
        {
            StartCoroutine(SpawnWave());
            waveNum++;
            timer = timeBetweenWaves;
        }

        timer -= Time.deltaTime;
	}

    //spawns n number of enemies
    IEnumerator SpawnWave() 
    {
        int numToSpawn = wavePattern[waveNum];

        for (int i = 0; i < numToSpawn / spawnpoints.Length; i++)
        {
            foreach (Transform spawnpoint in spawnpoints)
            {
                SpawnEnemy(spawnpoint, zombiePrefab);
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        for (int i = 0; i < numToSpawn % spawnpoints.Length; i++)
        {
            int rand = Random.Range(0, spawnpoints.Length);
            SpawnEnemy(spawnpoints[rand], zombiePrefab);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(Transform spawnpoint, GameObject enemyType)
    {
        GameObject enemy = Instantiate(enemyType, spawnpoint.position, spawnpoint.rotation);
        enemy.transform.parent = enemies;
    }
}
