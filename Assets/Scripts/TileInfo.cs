using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
    public bool rotatable = false;
    public Sprite[] sprites;
    public bool join_surrounding;
    public bool takes_damage;
    GridController control;
    public Vector2Int pos;
    public int index;


    public void Start()
    {
        control = GetComponentInParent<GridController>();
        Debug.Log(pos);
        render();
    }

    public void render()
    {
        index = 0;
        if (join_surrounding)
        {
            Vector2Int[] directions = { new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0,0) };
            foreach(Vector2Int dir in directions)
            {
                Vector2Int new_pos = pos + dir + new Vector2Int(0,0);
                Debug.Log(dir);
                Debug.Log((int)control.get(new_pos.x, new_pos.y));
                if (new_pos.x < control.getWidth() && new_pos.x >= 0 && new_pos.y < control.getWidth() && new_pos.y >= 0 && control.get(new_pos.x, new_pos.y) == Tile.Wall )
                {
                    
                    break;
                }
                index++;
            }
        }
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
