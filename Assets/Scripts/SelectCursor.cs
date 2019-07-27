using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCursor : MonoBehaviour {

    public new Camera camera;
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
        if (buildManager.buildingMode)
        {
            cursorRenderer.enabled = true;
            rangeCircleRenderer.enabled = true;

            float range = buildManager.getTowerToBuild().GetComponent<Tower>().getProperties().range;
            rangeCircle.transform.localScale = new Vector2(range, range);

            Vector3 roundedPos = getRoundedPos();
            transform.position = roundedPos; //sets popup at correct location

            if (buildManager.canBuildTower(roundedPos))
            {
                rangeCircleRenderer.material.color = Color.yellow;
                cursorRenderer.material.color = Color.yellow;
            }
            else
            {
                rangeCircleRenderer.material.color = Color.red;
                cursorRenderer.material.color = Color.red;
            }
        }
        else
        {
            cursorRenderer.enabled = false;
            if (buildManager.getSelectedTower() != null)
            {
                rangeCircleRenderer.enabled = true;

                float range = buildManager.getSelectedTower().GetComponent<Tower>().getProperties().range;
                rangeCircle.transform.localScale = new Vector2(range, range);

                transform.position = buildManager.getSelectedTower().transform.position;
            }
            else
            {
                rangeCircleRenderer.enabled = false;
            }
        }
    }

    public Vector3 getRoundedPos()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 roundedPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
        return roundedPos;
    }
}
