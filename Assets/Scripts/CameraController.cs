using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private new Camera camera;

    private float maxSize; //max size is set automatically by gridcontroller
    public float minSize;
    public float movementSpeed;
    public float scrollSpeed;

    [Header("Unity Setup")]
    public GridController grid;

	// Use this for initialization
	void Start ()
    {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //camera size adjustment
        float cameraSize = camera.orthographicSize;
        cameraSize -= Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime;
        cameraSize = Mathf.Min(maxSize, cameraSize);
        cameraSize = Mathf.Max(minSize, cameraSize);
        camera.orthographicSize = cameraSize;

        //calculating camera position bounds 
        float cameraWidth = cameraSize * camera.aspect;
        float minX = cameraWidth - 0.5f;
        float maxX = (grid.getWidth() - 0.5f) - cameraWidth;
        float minY = cameraSize - 0.5f;
        float maxY = (grid.getHeight() - 0.5f) - cameraSize;
        
        //keeps camera within bounds if size changed
        Vector3 adjustedPosition = camera.transform.position;
        adjustedPosition.y = Mathf.Max(minY, adjustedPosition.y);
        adjustedPosition.y = Mathf.Min(maxY, adjustedPosition.y);
        adjustedPosition.x = Mathf.Max(minX, adjustedPosition.x);
        adjustedPosition.x = Mathf.Min(maxX, adjustedPosition.x);

        camera.transform.position = adjustedPosition;

        //calculating camera movement
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movementVector = movementVector * movementSpeed * Time.deltaTime * (cameraSize / 5f);
        Vector3 futurePosition = camera.transform.position + movementVector;

        //prevents camera from leaving bounds
        if (futurePosition.y < minY || futurePosition.y > maxY)
        {
            futurePosition.y = camera.transform.position.y;
        }
        if (futurePosition.x < minX || futurePosition.x > maxX)
        {
            futurePosition.x = camera.transform.position.x;
        }

        camera.transform.position = futurePosition;
    }

    //adjusts boundaries for the camera's size, set by the gridcontroller
    public void setMaxSize(float size)
    {
        maxSize = size;
    }
}
