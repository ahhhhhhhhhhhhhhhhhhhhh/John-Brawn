﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    [HideInInspector()]
    public WaveInfo[] waves;
    [HideInInspector()]
    public Direction[] spawningDirections;

    private float timer;
    private int waveNum = 0;

    [Header("Unity Setup Stuff")]
    public Transform enemies; //parent of all enemies
    public GameObject zombiePrefab;
    public GameObject bigZombiePrefab;
    private GameObject[] zombieTypes;

    private GridController grid;

    public Text waveCountdown;

    // Use this for initialization
    void Start ()
    {
        timer = waves[0].delay;

        zombieTypes = new GameObject[] { zombiePrefab, bigZombiePrefab };

        grid = GetComponent<GridController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= 0 && waveNum < waves.Length)
        {
            StartCoroutine(SpawnWave());
            waveNum++;
            if (waveNum < waves.Length)
            {
                timer = waves[waveNum].delay;
            }
        }

        timer -= Time.deltaTime;
        
        if (timer < 0 && waveNum >= waves.Length) //means all waves have finished
        {
            waveCountdown.enabled = false;
        }
        waveCountdown.text = timer.ToString();
    }

    //spawns next wave on enemies from wave pattern
    IEnumerator SpawnWave() 
    {
        float spawnDelay = waves[waveNum].spawnDelay;

        int[] toSpawn = new int[] { waves[waveNum].numZombies, waves[waveNum].numBigZombies };
        System.Random rand = new System.Random();

        int totalEnemies = Sum(toSpawn);
        for (int i = 0; i < totalEnemies; i++)
        {
            Direction direction = spawningDirections[Random.Range(0, spawningDirections.Length)];

            int randIndex = rand.Next(toSpawn.Length);
            while (toSpawn[randIndex] == 0)
            {
                randIndex = rand.Next(toSpawn.Length);
            }

            SpawnEnemy(direction, zombieTypes[randIndex]);
            toSpawn[randIndex]--;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(Direction direction, GameObject enemyType)
    {
        Vector3 v1 = new Vector3(); //placeholder value
        Vector3 v2 = new Vector3(); //placeholder value

        float buffer = 0.5f; //distance from edge of grid that zombies spawn

        if (direction == Direction.Up)
        {
            v1 = new Vector3(-1 * buffer, grid.getHeight() - 0.5f + buffer);
            v2 = new Vector3(grid.getWidth() - 0.5f + buffer, grid.getHeight() - 0.5f + buffer);
        }
        else if (direction == Direction.Right)
        {
            v1 = new Vector3(grid.getWidth() - 0.5f + buffer, -1 * buffer);
            v2 = new Vector3(grid.getWidth() - 0.5f + buffer, grid.getHeight() - 0.5f + buffer);
        }
        else if (direction == Direction.Down)
        {
            v1 = new Vector3(-1 * buffer, -1 * buffer);
            v2 = new Vector3(grid.getWidth() - 0.5f + buffer, -1 * buffer);
        }
        else if (direction == Direction.Left)
        {
            v1 = new Vector3(-1 * buffer, -1 * buffer);
            v2 = new Vector3(-1 * buffer, grid.getHeight() - 0.5f + buffer);
        }

        Vector3 spawnpoint = Vector3.Lerp(v1, v2, Random.Range(1f, 0f));

        GameObject enemy = Instantiate(enemyType, spawnpoint, new Quaternion(0, 0, 0, 0));
        enemy.transform.parent = enemies;
    }

    //just a small helper variable for spawnwave
    private int Sum(int[] arr)
    {
        int sum = 0;
        foreach (int n in arr)
        {
            sum += n;
        }
        return sum;
    }


    //returns true if all waves have been spawned
    public bool Done()
    {
        return waveNum >= waves.Length;
    }
}
