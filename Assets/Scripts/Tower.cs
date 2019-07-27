using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [Header("Tower Properties")]
    public TowerInfo properties;
    private float fireTimer = 0f;

    private GameObject enemies; //parent of all enemies in game

    [Header("Unity Setup Stuff (don't change)")]
    public Transform turret; //part of the tower that rotates
    public Transform firePoint; //where bullets spawn
    public GameObject bulletPrefab;

    private BuildingManager buildManager;

	// Use this for initialization
	void Start ()
    {
        enemies = GameObject.Find("Enemies");
        buildManager = GameObject.Find("Level Control").GetComponent<BuildingManager>();
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
                fireTimer = 1 / properties.fireRate;
            }
        }

        fireTimer -= Time.deltaTime;
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, properties.range);
    }

    void shootAt(Transform target)
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.setTarget(target);
        bullet.setDamage(properties.damage);
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

            if (dist <= properties.range && dist < min)
            {
                min = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }

    private void OnMouseDown()
    {
        buildManager.selectTower(this);
    }
}
