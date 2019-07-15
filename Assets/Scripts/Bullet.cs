using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10f;

    private Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (target == null) //means target got killed by other tower or reached the end
        {
            Destroy(gameObject);
            return;
        }

        Vector3 diff = target.position - transform.position;
        float nextMovement = speed * Time.deltaTime;

        if (nextMovement >= diff.magnitude)
        {
            hitTarget();
            return;
        }

        transform.Translate(diff.normalized * nextMovement, Space.World);
	}

    void hitTarget()
    {
        Destroy(gameObject);
    }
}
