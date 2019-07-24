using System.Collections;
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

    public bool on = true; //way to turn wave spawning off for testing purposes

    [Header("Wave Properties")]
    public WaveInfo[] waves;
    public Direction direction;

    private float timer;
    private int waveNum = 0;

    [Header("Unity Setup Stuff (don't change)")]
    public Transform enemies; //parent of all enemies
    public GameObject zombiePrefab;

    public GridController grid;

    public Text waveCountdown;

    // Use this for initialization
    void Start ()
    {
        timer = waves[0].delay;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= 0 && on && waveNum < waves.Length)
        {
            StartCoroutine(SpawnWave(direction));
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
    IEnumerator SpawnWave(Direction dir) 
    {
        int numToSpawn = waves[waveNum].numZombies;
        float spawnDelay = waves[waveNum].spawnDelay;

        for (int i = 0; i < numToSpawn; i++)
        {
            Vector3 v1 = new Vector3(); //placeholder value
            Vector3 v2 = new Vector3(); //placeholder value

            if (dir == Direction.Up)
            {
                v1 = new Vector3(-0.5f, grid.getHeight() - 0.5f);
                v2 = new Vector3(grid.getWidth() - 0.5f, grid.getHeight() - 0.5f);
            }
            else if (dir == Direction.Right)
            {
                v1 = new Vector3(grid.getWidth() - 0.5f, -0.5f);
                v2 = new Vector3(grid.getWidth() - 0.5f, grid.getHeight() - 0.5f);
            }
            else if (dir == Direction.Down)
            {
                v1 = new Vector3(-0.5f, -0.5f);
                v2 = new Vector3(grid.getWidth() - 0.5f, -0.5f);
            }
            else if (dir == Direction.Left)
            {
                v1 = new Vector3(-0.5f, -0.5f);
                v2 = new Vector3(-0.5f, grid.getHeight() - 0.5f);
            }

            Vector3 spawnpoint = Vector3.Lerp(v1, v2, Random.Range(1f, 0f));
            SpawnEnemy(spawnpoint, zombiePrefab);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(Vector3 spawnpoint, GameObject enemyType)
    {
        GameObject enemy = Instantiate(enemyType, spawnpoint, new Quaternion(0, 0, 0, 0));
        enemy.transform.parent = enemies;
    }
}
