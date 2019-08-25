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
    private Transform target;

    [Header("Unity Setup Stuff")]
    public Image healthBar;
    public GameObject deathEffect;
    public Sprite[] sprites; //up, right, down, left

    private MoneyManager moneyManager;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();

        pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
        repath();

        moneyManager = GameObject.Find("Level Control").GetComponent<MoneyManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            moneyManager.add(reward);
            return;
        }

        healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);

        faceTarget();

        if (Vector3.Distance(target.position, transform.position) < 0.1)
        {
            if (checkEndpoint())
            {
                Destroy(gameObject);
                return;
            }
            repath();
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}

    public void hit(float damage)
    {
        health -= damage;
    }

    public void repath()
    {
        target = pathfinder.getNextNode(transform);
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
        Vector3 diff = target.position - transform.position;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if (diff.x > 0)
            {
                spriteRenderer.sprite = sprites[1]; //right
            }
            else
            {
                spriteRenderer.sprite = sprites[3]; //left
            }
        }
        else
        {
            if (diff.y > 0)
            {
                spriteRenderer.sprite = sprites[0]; //up
            }
            else
            {
                spriteRenderer.sprite = sprites[2]; //down

            }
        }
    }
}
