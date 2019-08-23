using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    //returns all enemies within a map square
    public List<GameObject> getEnemies(int x, int y)
    {
        List<GameObject> enemies = new List<GameObject>();

        Vector3 corner1 = new Vector3(x - 0.5f, y - 0.5f, 0);
        Vector3 corner2 = new Vector3(x + 0.5f, y + 0.5f, 0);

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 childPos = child.position;

            if (childPos.x > corner1.x && childPos.x < corner2.x && childPos.y > corner1.y && childPos.y < corner2.y)
            {
                enemies.Add(child.gameObject);
            }
        }

        return enemies;
    }
}
