﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCursor : MonoBehaviour {

    public Camera camera;
    public BuildingManager buildManager;

    private Transform rangeCircle;

    private Renderer cursorRenderer;
    private Renderer rangeCircleRenderer;

    void Start()
    {
        rangeCircle = transform.GetChild(0);
        cursorRenderer = GetComponent<Renderer>();
        rangeCircleRenderer = rangeCircle.GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update ()
    {
        cursorRenderer.enabled = buildManager.buildingMode;
        rangeCircleRenderer.enabled = buildManager.buildingMode;

        if (cursorRenderer.enabled)
        {
            Tower towerToBuild = buildManager.getTowerToBuild().GetComponent<Tower>();
            rangeCircle.transform.localScale = new Vector2(towerToBuild.range, towerToBuild.range);

            transform.position = getRoundedPos();
        }
    }

    public Vector3 getRoundedPos()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 roundedPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
        return roundedPos;
    }
}
