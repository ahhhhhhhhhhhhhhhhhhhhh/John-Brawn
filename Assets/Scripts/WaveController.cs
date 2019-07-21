using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {

    public bool on = true; //way to turn wave spawning off for testing purposes

    [Header("Wave Properties")]
    public WaveInfo[] waves;

    private float timer;
    private int waveNum = 0;

    [Header("Unity Setup Stuff (don't change)")]
    public Transform enemies; //parent of all enemies
    public GameObject zombiePrefab;

    public Transform spawnpointParent;
    private Transform[] spawnpoints;

    public Text waveCountdown;

    // Use this for initialization
    void Start ()
    {
        timer = waves[0].delay;

        spawnpoints = new Transform[spawnpointParent.childCount];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = spawnpointParent.GetChild(i);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= 0 && on && waveNum < waves.Length)
        {
            StartCoroutine(SpawnWave());
            waveNum++;
            if (waveNum < waves.Length)
            {
                timer = waves[waveNum].delay;
            }
        }

        timer -= Time.deltaTime;
        if (timer > 0) //timer being negative means all waves are done
        {
            waveCountdown.text = timer.ToString();
        }
	}

    //spawns next wave on enemies from wave pattern
    IEnumerator SpawnWave() 
    {
        int numToSpawn = waves[waveNum].numZombies;
        float spawnDelay = waves[waveNum].spawnDelay;

        for (int i = 0; i < numToSpawn / spawnpoints.Length; i++)
        {
            foreach (Transform spawnpoint in spawnpoints)
            {
                SpawnEnemy(spawnpoint, zombiePrefab);
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        int[] rand = getShuffledArray(spawnpoints.Length);
        for (int i = 0; i < numToSpawn % spawnpoints.Length; i++)
        {
            SpawnEnemy(spawnpoints[rand[i]], zombiePrefab);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(Transform spawnpoint, GameObject enemyType)
    {
        GameObject enemy = Instantiate(enemyType, spawnpoint.position, spawnpoint.rotation);
        enemy.transform.parent = enemies;
    }

    //returns shuffled array of length n
    private int[] getShuffledArray(int n)
    {
        System.Random rand = new System.Random();
        int[] arr = new int[n];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i;
        }
        for (int i = 0; i < arr.Length; i++)
        {
            int index = rand.Next(n);
            int temp = arr[i];
            arr[i] = arr[index];
            arr[index] = temp;
        }
        return arr;
    }
}
