using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCursor : MonoBehaviour {

    public Camera camera;
    public BuildingManager buildManager;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        renderer.enabled = buildManager.buildingMode;

        transform.position = getRoundedPos();
    }

    public Vector3 getRoundedPos()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 roundedPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
        return roundedPos;
    }
}
