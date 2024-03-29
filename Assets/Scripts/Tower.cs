﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour {

    [Header("Tower Properties")]
    public string type;
    public TowerInfo[] properties; //each set of properties in array corresponds to upgrade level
    private int level = 0;
    private float fireTimer = 0f;

    private GameObject enemies; //parent of all enemies in game

    [Header("Unity Setup Stuff (don't change)")]
    public Transform turret; //part of the tower that rotates
    public Transform firePoint; //where bullets spawn
    public GameObject bulletPrefab;

    public Sprite[] turretSprites;
    public Sprite[] baseSprites;
    private SpriteRenderer turretRenderer;
    private SpriteRenderer baseRederer;

    private BuildingManager buildManager;

	// Use this for initialization
	void Start ()
    {
        enemies = GameObject.Find("Enemies");
        buildManager = GameObject.Find("Level Control").GetComponent<BuildingManager>();
        turretRenderer = transform.Find("Turret").GetComponent<SpriteRenderer>();
        baseRederer = transform.Find("Base").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Transform target = getNearestEnemy();
        
        if (target != null) //there is an enemy within range
        {
            lookAt(target);
            if (fireTimer <= 0)
            {
                shootAt(target);
                fireTimer = 1 / properties[level].fireRate;
            }
        }

        fireTimer -= Time.deltaTime;
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, properties[level].range);
    }

    void shootAt(Transform target)
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.setTarget(target);
        bullet.setDamage(properties[level].damage);
    }

    //points turret towards target if within range
    void lookAt(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        turret.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    //returns transform of nearest enemy within range. If none in range, returns null
    Transform getNearestEnemy()
    {
        float min = int.MaxValue;
        Transform nearest = null;

        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            Transform enemy = enemies.transform.GetChild(i);
            Vector3 diff = enemy.position - transform.position;
            float dist = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y);

            if (dist <= properties[level].range && dist < min)
            {
                min = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }

    public void upgrade()
    {
        if (level < properties.Length - 1)
        {
            level++;
            replaceSprites(turretRenderer, turretSprites);
            replaceSprites(baseRederer, baseSprites);
        }
    }

    private void replaceSprites(SpriteRenderer renderer, Sprite[] images)
    {
        if (level < images.Length && images[level] != null) //if there isn't an upgrade-specific image, just keeps old one
        {
            renderer.sprite = images[level];
        }
    }

    //returns properties tower at current upgrade level
    public TowerInfo getProperties()
    {
        return properties[level];
    }

    public int getLevel()
    {
        return level;
    }

    public Vector2 getLocation() {
        return transform.position;
    }

    public void sell()
    {
        Destroy(gameObject);
    }

    //if a tower is clicked, it sends a reference of itself to the build manager
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //makes sure mouse isn't over a UI element
        {
            buildManager.selectTower(gameObject);
        }
    }

    //returns total amount of money spent on the tower (initial cost + upgrades)
    public int getMoneyInvested()
    {
        int invested = 0;

        for (int i = 0; i <= level; i++)
        {
            invested += properties[i].cost;
        }

        return invested;
    }
}
