using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour {

    [Header("Zombie Attributes")]
    public float speed = 2f;
    public float maxHealth = 100f;
    public int reward = 75;
    private float health;

    private Pathfinder pathfinder;
    private Vector3 target;

    [Header("Unity Setup Stuff")]
    public Image healthBar;
    public GameObject deathEffect;
    public Sprite[] sprites; //up, right, down, left

    private PlayerData player;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();

        pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
        repath();

        player = GameObject.Find("Level Control").GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            player.addMoney(reward);
            return;
        }

        healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);

        faceTarget();

        if (Vector3.Distance(target, transform.position) < 0.1)
        {
            if (checkEndpoint())
            {
                Destroy(gameObject);
                player.loseLife();
                return;
            }
            repath();
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

    public void hit(float damage)
    {
        health -= damage;
    }

    public void repath()
    {
        target = pathfinder.getNextNode(transform).position;

        float shiftScale = 0.25f;
        Vector3 shiftedTarget = new Vector3(target.x + Random.Range(-shiftScale, shiftScale), target.y + Random.Range(-shiftScale, shiftScale));

        target = shiftedTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, target);
    }

    //checks if zombie has reached endpoint
    private bool checkEndpoint()
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        return pos == pathfinder.endpoint;
    }

    //changes zombie sprite so it faces target
    private void faceTarget()
    {
        Vector3 diff = target - transform.position;
        int index = -1;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if (diff.x > 0)
            {
                index = 1; //right
            }
            else
            {
                index = 3; //left
            }
        }
        else
        {
            if (diff.y > 0)
            {
                index = 0; //up
            }
            else
            {
                index = 2; //down
            }
        }
        if (sprites[index] != null)
        {
            spriteRenderer.sprite = sprites[index];
        }
    }
}
