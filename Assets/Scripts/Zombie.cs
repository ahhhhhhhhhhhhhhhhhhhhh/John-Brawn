using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour {

    [Header("Zombie Attributes")]
    public float speed = 2f;
    public float maxHealth = 100f;
    private float health;

    private Pathfinder pathfinder;
    private Path path;
    private int pathIndex;
    public float repathFrequency; //how often zombie repaths
    private float repathTimer;

    public Vector2Int endpoint;

    [Header("Debugging")]
    public bool drawPath;

    [Header("Unity Setup Stuff")]
    public Image healthBar;
    public GameObject deathEffect;
    public Sprite[] sprites; //up, right, down, left

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();

        pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
        repathTimer = repathFrequency;

        path = pathfinder.getPath(transform, endpoint);
        pathIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            return;
        }

        healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);

        if (path == null)
        {
            path = pathfinder.getPath(transform, endpoint);
            pathIndex = 0;
        }

        Transform target = ((Node)path.nodes[pathIndex]).gameObject.transform;

        if (repathTimer <= 0)
        {
            path = pathfinder.getPath(target, endpoint);
            pathIndex = 0;
            repathTimer = repathFrequency;
        }

        //makes zombie face direction it is moving
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

        if (Vector3.Distance(target.position, transform.position) < 0.1)
        {
            if (pathIndex < path.nodes.Count - 1)
            {
                pathIndex++;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        repathTimer -= Time.deltaTime;
	}

    public void setEndpoint(Vector2Int endpoint)
    {
        this.endpoint = endpoint;
    }

    public void setEndpoint(Transform endpoint)
    {
        this.endpoint = new Vector2Int(Mathf.RoundToInt(endpoint.position.x), Mathf.RoundToInt(endpoint.position.y));
    }

    public void hit(float damage)
    {
        health -= damage;
    }

    private void OnDrawGizmos()
    {
        if (drawPath && path != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, ((Node)path.nodes[0]).gameObject.transform.position);
            for (int i = 0; i < path.nodes.Count - 1; i++)
            {
                Node current = (Node)path.nodes[i];
                Node next = (Node)path.nodes[i + 1];
                Gizmos.DrawLine(current.gameObject.transform.position, next.gameObject.transform.position);
            }
        }
    }
}
